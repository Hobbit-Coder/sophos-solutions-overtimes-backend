namespace SophosSolutions.Overtimes.Application.Common.Models;

public class JwtDetails
{
    public Guid Sid { get; set; }
    public string? TokenType { get; set; }
    public string? AccessToken { get; set; }
    public DateTime Expires { get; set; }
}
