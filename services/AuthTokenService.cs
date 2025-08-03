using Microsoft.IdentityModel.JsonWebTokens;
using DotNetTest.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DotNetTest.Services;

class AuthTokenService : IAuthTokenService<AuthorizedUser>
{

    private IConfiguration _configuration;
    private JsonWebTokenHandler _tokenHandler;
    private SigningCredentials _signingCredentials;
    public AuthTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenHandler = new JsonWebTokenHandler();

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );
        _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
    public string GenerateToken(AuthorizedUser user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("role", user.role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(15), // Token expiry time
            SigningCredentials = _signingCredentials,
            Issuer = "payroll_backend",
            Audience = "payroll_frontend"
        };

        var token = this._tokenHandler.CreateToken(tokenDescriptor);
        return token;

        }

    public async Task<AuthorizedUser> ValidateTokenAsync(string token)
    {

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingCredentials.Key,
            ValidateIssuer = true,
            ValidIssuer = "payroll_backend",
            ValidateAudience = true,
            ValidAudience = "payroll_frontend",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1) // No clock skew
        };

        try
        {
            TokenValidationResult validationResult = await _tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
            if (validationResult.IsValid)
            {
                var claims = validationResult.ClaimsIdentity.Claims;
                Guid userId = Guid.Parse(claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? throw new SecurityTokenValidationException("User ID claim not found"));
                string role = claims.FirstOrDefault(c => c.Type == "role")?.Value ?? "regular";

                return new AuthorizedUser(userId, role);
            }
            else
            {
                throw new SecurityTokenValidationException("Invalid token");
            }
        }
        catch (Exception)
        {

            throw;
        }
        throw new NotImplementedException();
    }
}