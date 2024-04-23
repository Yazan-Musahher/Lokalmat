using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.OrderModule;
using Stripe;
using Stripe.Checkout;

namespace server.Controllers.Payment;

[Route("[controller]")]
[ApiController]
public class PaymentController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IConfiguration configuration, ApplicationDbContext context,
        ILogger<PaymentController> logger)
    {
        _configuration = configuration;
        _context = context;
        _logger = logger;
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

        [HttpPost("create-checkout-session")]
        public ActionResult Create(CreateSessionRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                _logger.LogError("User ID is null or empty when creating checkout session.");
                return BadRequest("User ID cannot be empty.");
            }

            if (request.ProductIds == null || !request.ProductIds.Any())
                return BadRequest("Product IDs list cannot be empty.");

            var domain = "https://lokalprodusert.com";
            var products = _context.Products
                .Where(p => request.ProductIds.Contains(p.Id))
                .ToList();

            if (products.Count != request.ProductIds.Count) return BadRequest("One or more products are not available.");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = products.Select(product => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(product.Price * 100),
                        Currency = "nok",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                            Images = new List<string> { product.ImageUrl },
                            Metadata = new Dictionary<string, string>
                            {
                                { "productId", product.Id.ToString() }
                            }
                        }
                    },
                    Quantity = 1
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "/order-success/?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/order-failure",
                ClientReferenceId = request.UserId // Set ClientReferenceId to userId
            };

            var service = new SessionService();
            try
            {
                var session = service.Create(options);
                return Ok(new { url = session.Url });
            }
            catch (StripeException e)
            {
                _logger.LogError("Stripe error: {Message}", e.Message);
                return BadRequest(new { error = e.StripeError.Message });
            }
        }

        [HttpGet("finalize-payment")]
        public async Task<IActionResult> FinalizePayment(string session_id)
        {
            if (string.IsNullOrEmpty(session_id))
            {
                _logger.LogWarning("Session ID is null or empty.");
                return BadRequest("Session ID cannot be empty.");
            }

            var sessionService = new SessionService();
            try
            {
                var session = await sessionService.GetAsync(session_id, new SessionGetOptions
                {
                    Expand = new List<string> { "line_items.data.price.product" } // Make sure this path is correct
                });

                if (session == null)
                {
                    _logger.LogWarning($"Session not found for ID: {session_id}");
                    return StatusCode(404, "Session not found.");
                }

                if (session.PaymentStatus != "paid")
                {
                    _logger.LogWarning($"Payment not successful for session ID: {session_id}, Status: {session.PaymentStatus}");
                    return StatusCode(402, "Payment not successful.");
                }

                var userId = session.ClientReferenceId;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError($"User ID not found in session data: {session_id}");
                    return StatusCode(404, "User ID not found in session data.");
                }

                _logger.LogInformation($"Creating order for user ID: {userId}");
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow
                };

                var orderItems = new List<OrderItem>();
                foreach (var item in session.LineItems.Data)
                {
                    if (item.Price == null || item.Price.Product == null || !item.Price.Product.Images.Any())
                    {
                        _logger.LogError($"Incomplete product data for session: {session_id}");
                        return StatusCode(400, "Incomplete product data.");
                    }

                    if (!item.Price.Product.Metadata.TryGetValue("productId", out var productIdString) ||
                        !Guid.TryParse(productIdString, out var productId))
                    {
                        _logger.LogError($"Product ID is missing or invalid in the metadata for session: {session_id}");
                        return StatusCode(400, "Product ID is missing or invalid.");
                    }

                    var imageUrl = item.Price.Product.Images.FirstOrDefault(); // Getting the first image URL

                    orderItems.Add(new OrderItem
                    {
                        ProductId = productId,
                        Quantity = (int)item.Quantity,
                        UnitPrice = (decimal)item.Price.UnitAmount / 100,
                        ImageUrl = imageUrl // Set the ImageUrl here
                    });
                }

                order.OrderItems = orderItems;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order created successfully: {order.Id}");

                return Ok(new { Message = "Order created successfully", OrderId = order.Id });
            }
            catch (StripeException e)
            {
                _logger.LogError(e, $"Stripe error finalizing payment for session: {session_id}");
                return StatusCode(500, "Stripe error: " + e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error finalizing payment for session: {session_id}. Exception message: {ex.Message}");
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpGet("order-history/{userId}")]
        public async Task<IActionResult> GetOrderHistory(string userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    Items = o.OrderItems.Select(oi => new 
                    {
                        oi.ProductId, 
                        oi.Quantity, 
                        oi.UnitPrice,
                        oi.ImageUrl 
                    })
                })
                .ToListAsync();

            return Ok(orders);
        }
    }