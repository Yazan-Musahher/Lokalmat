using Mapster;

using TransportHub.Domain.TransporterAggregate;
using TransportHub.Domain.TransporterAggregate.Entities;
using TransportHub.Domain.TransporterAggregate.ValueObjects;
using TransportHub.Infrastructure.Persistence.Models;

namespace TransportHub.Infrastructure.Persistence.Configurations;


public class TransporterDtoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransporterAggregate, TransporterDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CompanyName, src => src.CompanyName)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AddressDto.Id, src => src.Address.Id.Value)
            .Map(dest => dest.AddressDto.City, src => src.Address.City)
            .Map(dest => dest.AddressDto.ZipCode, src => src.Address.ZipCode);

        config.NewConfig<TransporterDto, TransporterAggregate>()
            .MapWith(dto => TransporterAggregate.Create(
                TransporterId.Create(dto.Id),
                dto.CompanyName ?? string.Empty,
                dto.Description ?? string.Empty,
                TransporterAddress.Create(
                    TransporterAddressId.Create(dto.AddressDto.Id),
                    dto.AddressDto.City, 
                    dto.AddressDto.ZipCode)));
    }
}
