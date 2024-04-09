using Mapster;
using Users.Application.Authentication.Commands;
using Users.Contacts.Authentication;
using Users.Domain.UserAggregate;

namespace Users.API.Common.Mapping;

public class AuthenticationbMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<(User, string Token), AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.Item1.Id.Value)
            .Map(dest => dest.FirstName, src => src.Item1.FirstName)
            .Map(dest => dest.LastName, src => src.Item1.LastName)
            .Map(dest => dest.Email, src => src.Item1.Email)
            .Map(dest => dest.Role, src => src.Item1.Role.ToString())
            .Map(dest => dest.Token, src => src.Item2)
            .Map(dest => dest.CreatedAt, src => src.Item1.CreatedAt);
       
    }
}
