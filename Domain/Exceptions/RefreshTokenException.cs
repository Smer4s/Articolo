namespace Domain.Exceptions;

public class RefreshTokenException(string refreshToken) 
    : Exception(refreshToken)
{ }
