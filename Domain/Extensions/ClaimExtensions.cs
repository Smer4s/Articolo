using Domain.Entities;
using System.Security.Claims;

namespace Domain.Extensions;

public static class ClaimExtensions
{
    public static int GetId(this ClaimsPrincipal principal)
    {
        return principal.Claims.GetId();
    }

    public static int GetId(this IEnumerable<Claim> claims)
    {
        var strId = claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

        if (int.TryParse(strId, out int value))
            return value;

        return 0;
    }
}
