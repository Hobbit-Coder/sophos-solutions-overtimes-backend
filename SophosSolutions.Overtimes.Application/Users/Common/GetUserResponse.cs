using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Users.Common;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public IList<string> Role { get; set; } = new List<string>();
    public Area? Area { get; set; }
}
