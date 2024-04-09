using Administration.Application.Users.Commands;
using Administration.Contracts.UserRoleAssignment;
using Administration.Domain.AdminAggregate;
using Mapster;

namespace Administration.API.Common.Mapping;
public class AsignRoleToUserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AssignRoleToUserRequest, AsignRoleToUserCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role);


        config.ForType<User, AssignRoleToUserResponse>()
            .Map(dest => dest.UserId, src => src.Id.Value)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role)
            .Map(dest => dest.AssignedAt, src => src.CreatedAt);

    }
}