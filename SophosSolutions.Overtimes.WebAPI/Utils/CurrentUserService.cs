using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using System.Security.Claims;

namespace SophosSolutions.Overtimes.WebAPI.Utils;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid);
            if (!string.IsNullOrEmpty(id))
                return new Guid(id);
            return Guid.Empty;
        }
    }
}
