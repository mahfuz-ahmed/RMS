
using FinancialManagementSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagementSystem.Infrastructure
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add new refresh token
        public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            _dbContext.RefreshToken.Add(refreshToken);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        // Get refresh token by token string
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshToken
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        // Update refresh token (for rotation: mark used/revoked)
        public async Task<RefreshToken> UpdateAsync(RefreshToken refreshToken)
        {
            _dbContext.RefreshToken.Update(refreshToken);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }
    }
}


