namespace DotNetTest.Services;

public interface IAuthTokenService<T>
{
    string GenerateToken(T claims);
    Task<T> ValidateTokenAsync(string token);
}