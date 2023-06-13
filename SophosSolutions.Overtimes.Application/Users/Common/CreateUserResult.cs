namespace SophosSolutions.Overtimes.Application.Users.Common;

public record CreateUserReponse(
    Guid Id,
    string UserName,
    string Name,
    string LastName,
    string Email
);