using Mapster;

using Payments.Domain.OrderAggregate.ValueObjects;
using Payments.Domain.PaymentAggregate;
using Payments.Infrastructure.Models;

namespace Payments.Infrastructure.Presistence.Configurations;

public sealed class PaymentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PaymentAggregate, PaymentDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CustomerId, src => src.CustomerId.Value)
            .Map(dest => dest.OrderId, src => src.OrderId.Value)
            .Map(dest => dest.PaymentMethod, src => src.PaymentMethod)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.PaymentStatus, src => src.PaymentStatus.ToString())
            .Map(dest => dest.PaymentDate, src => src.PaymentDate);

        config.NewConfig<PaymentDto, PaymentAggregate>()
            .MapWith(dto => PaymentAggregate.Create(
                PaymentId.Create(dto.Id),
                CustomerId.Create(dto.CustomerId),
                OrderId.Create(dto.OrderId),
                dto.PaymentMethod,
                dto.Amount,
                dto.PaymentDate));
    }
}