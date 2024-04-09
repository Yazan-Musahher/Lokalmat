namespace Users.Contacts.Authentication;

public record AuthenticationResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string Token,
    DateTime CreatedAt
    );