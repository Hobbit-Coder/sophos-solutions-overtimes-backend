namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class CreateUserReponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
