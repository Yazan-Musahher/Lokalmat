namespace Administration.Contracts.UserRoleAssignment;

public record AssignRoleToUserResponse(
    Guid UserId,
    string Email, 
    string Role,
    DateTime AssignedAt
    );