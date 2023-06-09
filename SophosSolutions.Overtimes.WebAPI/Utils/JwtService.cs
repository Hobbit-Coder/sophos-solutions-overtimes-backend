using Microsoft.IdentityModel.Tokens;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SophosSolutions.Overtimes.WebAPI.Utils;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public JwtService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task<JwtDetails> GenerateJWT(User user)
    {
        var roles = await _unitOfWork.UserRepository.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, $"{user.Name} {user.LastName}")
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var expiresIn = DateTime.Now.AddMinutes(720);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresIn,
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        var jwtDetails = new JwtDetails();
        jwtDetails.AccessToken = jwt;
        jwtDetails.Expires = expiresIn;
        jwtDetails.Sid = user.Id;
        jwtDetails.TokenType = "Bearer";

        return jwtDetails;
    }
}
