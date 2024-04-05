using Domain.Entities;
using System.Security.Claims;

namespace WebAPI.Extensions;

public static class ClaimExtensions
{
	public static int GetId(this ClaimsPrincipal claims)
	{
		var strId = claims.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

		if (int.TryParse(strId, out int value))
			return value;

		return 0;
	}
}
