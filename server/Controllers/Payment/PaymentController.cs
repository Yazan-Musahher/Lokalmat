using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.OrderModule;

namespace server.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IConfiguration configuration, ApplicationDbContext context, ILogger<PaymentController> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost("create-checkout-session")]
        public ActionResult Create(List<Guid> productIds)
        {
            var domain = "http://localhost:3000";
            var products = _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToList();

            if (products.Count != productIds.Count)
            {
                return BadRequest("One or more products are not available.");
            }

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
                        },
                    },
                    Quantity = 1,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "/order-success/?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/order-failure",
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
                    Expand = new List<string> { "line_items.data.price.product" }
                });

                if (session == null || session.PaymentStatus != "paid")
                {
                    _logger.LogWarning("Session not found or payment not successful for ID: {SessionId}", session_id);
                    return Redirect("/order-failure");
                }

                var lineItems = session.LineItems.Data;
                if (lineItems == null || !lineItems.Any())
                {
                    _logger.LogError("No line items found for session ID: {SessionId}", session_id);
                    return Redirect("/order-failure");
                }

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = session.ClientReferenceId, 
                    OrderDate = DateTime.UtcNow,
                };

                var orderItems = new List<OrderItem>();
                foreach (var item in lineItems)
                {
                    if (item.Price == null || item.Price.Product == null)
                    {
                        _logger.LogError("Incomplete price data for session: {SessionId}", session_id);
                        return Redirect("/order-failure");
                    }

                    if (!item.Price.Product.Metadata.TryGetValue("productId", out var productIdString) || !Guid.TryParse(productIdString, out var productId))
                    {
                        _logger.LogError("Product ID is missing or invalid in the metadata for session: {SessionId}", session_id);
                        return Redirect("/order-failure");
                    }

                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = productId,
                        Quantity = (int)item.Quantity,
                        UnitPrice = (decimal)item.Price.UnitAmount / 100
                    });
                }

                order.OrderItems = orderItems;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Redirect($"/order-success?orderId={order.Id}");
            }
            catch (StripeException e)
            {
                _logger.LogError(e, "Stripe error finalizing payment for session: {SessionId}", session_id);
                return Redirect("/order-failure");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finalizing payment for session: {SessionId}. Exception message: {Message}", session_id, ex.Message);
                return Redirect("/order-failure");
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
                    Items = o.OrderItems.Select(oi => new { oi.ProductId, oi.Quantity, oi.UnitPrice })
                })
                .ToListAsync();

            return Ok(orders);
        }
    }
}
