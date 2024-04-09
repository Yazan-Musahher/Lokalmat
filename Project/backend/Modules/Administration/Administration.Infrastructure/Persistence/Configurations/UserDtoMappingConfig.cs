using Administration.Domain.AdminAggregate;
using Administration.Domain.AdminAggregate.ValueObjects;
using Administration.Infrastructure.Persistence.Models;

using Mapster;

namespace Administration.Infrastructure.Persistence.Configurations;

public class UserDtoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.UserRole, src => src.Role.ToString())
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);

        config.NewConfig<UserDto, User>()
            .MapWith(dto => User.Create(
                UserId.Create(dto.Id),
                dto.Email ?? string.Empty,
                dto.CreatedAt));
    }
}
