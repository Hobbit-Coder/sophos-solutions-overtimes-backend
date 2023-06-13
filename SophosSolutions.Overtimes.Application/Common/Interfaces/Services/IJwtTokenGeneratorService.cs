using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Services;

public interface IJwtTokenGeneratorService
{
    Task<JwtDetails> Generate(User user);
}
