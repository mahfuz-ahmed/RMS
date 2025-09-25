

namespace FinancialManagementSystem.Core
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);

        Task<RefreshToken> UpdateAsync(RefreshToken refreshToken);
    }
}
