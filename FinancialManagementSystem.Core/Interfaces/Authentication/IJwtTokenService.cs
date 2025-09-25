
using System.Security.Claims;

namespace FinancialManagementSystem.Core
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(int userId, string jwtId);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        Task<(string JwtToken, string RefreshToken)> RefreshTokenAsync(string oldRefreshToken);
    }
}
