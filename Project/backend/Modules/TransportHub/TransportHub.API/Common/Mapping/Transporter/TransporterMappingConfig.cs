using Mapster;

using TransportHub.Application.Commands.RegisterTransporter;
using TransportHub.Contracts.Transporter.Register;
using TransportHub.Domain.TransporterAggregate;
using TransportHub.Domain.TransporterAggregate.Entities;
namespace TransportHub.API.Common.Mapping;

public class TransportHubMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<RegisterTransporterRequest, RegisterTransporterCommand>()
            .Map(dest => dest.CompanyName, src => src.CompanyName)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AddressCommnad, src => src.AddressRequest);

        config.ForType<TransporterAggregate, RegisterTransporterResponse>()
            .Map(dest => dest.TransporterId, src => src.Id.Value)
            .Map(dest => dest.AddressResponse.AddressId, src => src.Address.Id.Value)
            .Map(dest => dest.CompanyName, src => src.CompanyName)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AddressResponse, src => src.Address);
    
    }
}
