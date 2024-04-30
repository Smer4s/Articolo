using Ardalis.GuardClauses;
using Domain.Configurations;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Models.User;
using Domain.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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

    public ApiTokenModel RefreshTokens(User user, string refreshToken)
    {
        ValidateRefreshToken(user.RefreshToken, refreshToken);

        return GenerateTokens(user);
    }

    public static int GetIdFromTokenString(string accessToken) =>
        new JwtSecurityToken(accessToken).Claims.GetId();

    private static void ValidateRefreshToken(string? userRefreshToken, string refreshToken)
    {
        Guard.Against.NullOrWhiteSpace(userRefreshToken);

        Guard.Against.NotEqualsRefreshTokens(userRefreshToken, refreshToken);

        Guard.Against.ExpiredRefreshToken(refreshToken);
    }


    private static string GenerateRefreshToken(string accessToken)
    {
        var sb = new StringBuilder();

        var expirationDate = DateTime.Now.AddDays(30);
        var encodedDate = DateDecoder.EncodeExpirationDate(expirationDate);
        sb.Append(encodedDate);

        using (var rng = RandomNumberGenerator.Create())
        {
            var randomNumber = new byte[64];
            rng.GetBytes(randomNumber);
            sb.Append(Convert.ToBase64String(randomNumber));
        }

        sb.Append(accessToken.AsSpan(accessToken.Length - 6));

        return sb.ToString();
    }

    private string GenerateAccessToken(ClaimsIdentity identity)
    {
        var jwt = new JwtSecurityToken(
            claims: identity.Claims,
            expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

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
