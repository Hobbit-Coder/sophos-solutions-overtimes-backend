using Microsoft.AspNetCore.Identity;
using SophosSolutions.Overtimes.Application.Common.Models;

namespace SophosSolutions.Overtimes.Infrastructure.Common;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}
