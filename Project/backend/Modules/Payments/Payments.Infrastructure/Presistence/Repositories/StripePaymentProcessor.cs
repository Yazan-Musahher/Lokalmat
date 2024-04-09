using ErrorOr;
using Payments.Application.Payment.Models;
using Payments.Domain.PaymentAggregate;
using Stripe;
using Stripe.Checkout;
namespace Payments.Infrastructure.Presistence.Repositories;

public class StripePaymentProcessor : IPaymentProcessor
{

    public StripePaymentProcessor()
    {
    }

    public async Task<ErrorOr<PaymentResult?>> CreateCheckoutSessionAsync(
                        PaymentAggregate payment, 
                        CancellationToken cancellationToken = default)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {

                        UnitAmountDecimal = payment.Amount,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "T-shirt",
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = $"http://localhost:5018/payments/confirm-payment?paymentId={payment.Id.Value}&sessionId={{CHECKOUT_SESSION_ID}}",
            CancelUrl = "https://example.com/cancel",
        };

        try
        {
            var service = new SessionService();
            Session session = await service.CreateAsync(options, cancellationToken: cancellationToken);
            var result = new PaymentResult
            {
                SessionId = session.Id,
                Url = session.Url
            };
            return result;
        }
        catch (StripeException ex)
        {
            return Error.NotFound("Failed to create checkout session: " + ex.Message );
        }
        catch (Exception ex)
        {
            return Error.NotFound("An unexpected error occurred: " + ex.Message );
        }


    }


    /*public async Task<PaymentResult> ProcessPaymentAsync(PaymentAggregate payment, CancellationToken cancellationToken = default)
    {
        var customerService = new CustomerService();
        var customer = await customerService.CreateAsync(new CustomerCreateOptions
        {
            Email = "customer@example.com",
        });

        var options = new PaymentIntentCreateOptions
        {
            Customer = customer.Id,
            Amount = Convert.ToInt64(payment.Amount),
            Currency = "usd",
            PaymentMethod = payment.PaymentMethod,
            Description = $"Payment for Order {payment.OrderId}",
        };

       try
        {
            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            var confirmOptions = new PaymentIntentConfirmOptions
            {
                PaymentMethod = payment.PaymentMethod,
                ReturnUrl = "https://example.com",

            };

            paymentIntent = service.Confirm(paymentIntent.Id, confirmOptions);
            return new PaymentResult
            {
                Success = paymentIntent.Status == "succeeded",
                TransactionId = paymentIntent.Id,
                Message = paymentIntent.Status
            };
        }
        catch (StripeException ex)
        {
            return new PaymentResult
            {
                Success = false,
                Message = ex.Message
            };
        }
    }*/
}
