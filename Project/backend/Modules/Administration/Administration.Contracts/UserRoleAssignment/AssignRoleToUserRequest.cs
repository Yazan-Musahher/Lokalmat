namespace Administration.Contracts.UserRoleAssignment;

public record AssignRoleToUserRequest(
    Guid UserId,
    string Email, 
    string Role
    );