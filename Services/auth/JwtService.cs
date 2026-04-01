using his_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace his_backend.Services;

public interface IJwtService
{
   
    string TaoAccessToken(AppUser user);


    string TaoRefreshToken();

    ClaimsPrincipal? LayClaimsTuToken(string token);
}

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config) => _config = config;

    public string TaoAccessToken(AppUser user)
    {
        var jwt        = _config.GetSection("Jwt");
        var secretKey  = jwt["SecretKey"]  ?? throw new InvalidOperationException("Jwt:SecretKey chÆ°a cáº¥u hÃ¬nh");
        var issuer     = jwt["Issuer"]     ?? "his-backend";
        var audience   = jwt["Audience"]   ?? "his-app";
        var expiryMins = int.Parse(jwt["AccessTokenExpiryMinutes"] ?? "60");

        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

       var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Mand.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Mand.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("sdt", user.SoDienThoai ?? ""),
        };

        var token = new JwtSecurityToken(
            issuer:             issuer,
            audience:           audience,
            claims:             claims,
            notBefore:          DateTime.UtcNow,
            expires:            DateTime.UtcNow.AddMinutes(expiryMins),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string TaoRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            .Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    public ClaimsPrincipal? LayClaimsTuToken(string token)
    {
        var jwt       = _config.GetSection("Jwt");
        var secretKey = jwt["SecretKey"] ?? "";

        var validationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer           = true,
            ValidIssuer              = jwt["Issuer"] ?? "his-backend",
            ValidateAudience         = true,
            ValidAudience            = jwt["Audience"] ?? "his-app",
            ValidateLifetime         = false,  // Cho phép token hết hạn khi dùng để refresh
            ClockSkew                = TimeSpan.Zero
        };

        try
        {
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token, validationParams, out var validated);

            if (validated is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.OrdinalIgnoreCase))
                return null;

            return principal;
        }
        catch { return null; }
    }
}

