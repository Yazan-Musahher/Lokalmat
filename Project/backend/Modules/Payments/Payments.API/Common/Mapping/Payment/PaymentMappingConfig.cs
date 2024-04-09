using Mapster;

using Payments.Application.Payment.Commands.CreatePayment;
using Payments.Application.Payment.Models;
using Payments.Contracts.Payment.CreatePayment;
using Payments.Domain.PaymentAggregate;

namespace Payments.API.Common.Mapping.Payment
{
    public class PaymentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatePaymentRequest, CreatePaymentCommand>()
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.OrderId, src => src.OrderId)
                .Map(dest => dest.PaymentMethod, src => src.PaymentMethod)
                .Map(dest => dest.Amount, src => src.Amount);

            config.NewConfig<(PaymentAggregate, PaymentResult), CreatePaymentResponse>()
                .Map(dest => dest.PaymentId, src => src.Item1.Id.Value)
                .Map(dest => dest.CustomerId, src => src.Item1.CustomerId.Value)
                .Map(dest => dest.OrderId, src => src.Item1.OrderId.Value)
                .Map(dest => dest.Amount, src => src.Item1.Amount)
                .Map(dest => dest.PaymentMethod, src => src.Item1.PaymentMethod)
                .Map(dest => dest.DateTime, src => src.Item1.PaymentDate)
                .Map(dest => dest.PaymentResultResponse, src => src.Item2);
        }
    }
}
