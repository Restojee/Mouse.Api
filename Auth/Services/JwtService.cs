using Mouse.NET.Users.Common;

namespace Mouse.NET.Auth.Services;

public class JwtService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public JwtService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        var userIdClaim = this.httpContextAccessor.HttpContext.User.FindFirst(UserDetails.Id);
        if (userIdClaim != null)
        {
            return int.Parse(userIdClaim.Value);
        }
        return 0;
    }
}