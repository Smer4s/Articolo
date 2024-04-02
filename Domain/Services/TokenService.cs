using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain.Services;

public class TokenService(JwtSecurityTokenHandler tokenHandler)
{
	public ApiTokenModel GenerateTokens(User user)
	{
		var identity = GetIdentity(user);
		var accessToken = GenerateAccessToken(identity);
		var refreshToken = GenerateRefreshToken(accessToken);

		return new ApiTokenModel()
		{
			AccessToken = accessToken,
			RefreshToken = refreshToken,
		};
	}

	private string GenerateRefreshToken(string accessToken)
	{
		return "1j2nakniodj2lkdnklan2d";
	}

	private string GenerateAccessToken(ClaimsIdentity identity)
	{
		var jwt = new JwtSecurityToken( 
			claims: identity.Claims, 
			expires: DateTime.Now.AddMinutes(20));

		return tokenHandler.WriteToken(jwt);
	}

	private static ClaimsIdentity GetIdentity(User user)
	{
		IEnumerable<Claim> claims =
		[
			new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
			new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
		];

		return new ClaimsIdentity(
			claims,
			"Token",
			ClaimsIdentity.DefaultNameClaimType,
			ClaimsIdentity.DefaultRoleClaimType);
	}
}
