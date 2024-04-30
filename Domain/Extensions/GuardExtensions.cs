using Ardalis.GuardClauses;
using Domain.Exceptions;
using Domain.Utils;

namespace Domain.Extensions;

public static class GuardExtensions
{
    /// <summary>
    /// Throws an <see cref="RefreshTokenException"/> if two refresh tokens are not equal.
    /// </summary>
    /// <param name="clause"></param>
    /// <param name="userRefreshToken"></param>
    /// <param name="refreshToken"></param>
    /// <returns> <paramref name="refreshToken"/> if refresh tokens are equal.</returns>
    /// <exception cref="RefreshTokenException"></exception>
    /// 
    public static string NotEqualsRefreshTokens(this IGuardClause clause, string userRefreshToken, string refreshToken)
    {
        if (userRefreshToken != refreshToken)
            throw new RefreshTokenException(refreshToken);

        return refreshToken;
    }

    /// <summary>
    /// Throws an <see cref="RefreshTokenException"/> if refresh token is expired.
    /// </summary>
    /// <param name="clause"></param>
    /// <param name="refreshToken"></param>
    /// <returns><paramref name="refreshToken"/> if refresh token has valid expiration date.</returns>
    /// <exception cref="RefreshTokenException"></exception>
    public static string ExpiredRefreshToken(this IGuardClause clause, string refreshToken)
    {
        if (DateDecoder.DecodeExpirationDate(refreshToken) <= DateTime.Now)
            throw new RefreshTokenException(refreshToken);

        return refreshToken;
    }
}
