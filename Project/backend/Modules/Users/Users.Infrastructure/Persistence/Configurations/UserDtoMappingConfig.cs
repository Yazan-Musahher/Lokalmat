using Mapster;

using Payments.Domain.OrderAggregate.ValueObjects;

using Users.Domain.UserAggregate;
using Users.Domain.UserAggregate.Enums;
using Users.Infrastructure.Persistence.Models;


namespace Users.Infrastructure.Persistence.Configurations;

public class UserDtoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.UserRole, src => src.Role.ToString())
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);

        config.NewConfig<UserDto, User>()
            .MapWith(dto => User.Create(
                UserId.Create(dto.Id),
                dto.FirstName ?? string.Empty,
                dto.LastName ?? string.Empty,
                dto.Email ?? string.Empty,
                dto.Password ?? string.Empty,
                Enum.Parse<UserRole>(dto.UserRole),
                dto.CreatedAt));
    }
}
