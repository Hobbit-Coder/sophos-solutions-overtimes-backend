namespace SophosSolutions.Overtimes.Application.Common.Models;

public class JwtDetails
{
    public Guid Sid { get; set; }
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public DateTime Expires { get; set; }

    public JwtDetails(Guid sid, string tokenType, string accessToken, DateTime expires)
    {
        Sid = sid;
        TokenType = tokenType;
        AccessToken = accessToken;
        Expires = expires;
    }
}
