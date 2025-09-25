using FinancialManagementSystem.Core;
using Microsoft.EntityFrameworkCore;


namespace FinancialManagementSystem.Infrastructure
{
    internal class ChartOfAccountRepository: IChartOfAccountRepository
    {
        private readonly AppDbContext _context;

        public ChartOfAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChartOfAccount> AddChartOfAccountAsync(ChartOfAccount coa)
        {
            await _context.ChartOfAccounts.AddAsync(coa);
            await _context.SaveChangesAsync();
            return coa;
        }

        public async Task<ChartOfAccount> UpdateChartOfAccountAsync(ChartOfAccount coa)
        {
            _context.ChartOfAccounts.Update(coa);
            await _context.SaveChangesAsync();
            return coa;
        }

        public async Task<bool> DeleteUserAsync(ChartOfAccount coa)
        {
            _context.ChartOfAccounts.Remove(coa);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ChartOfAccount> ChartOfAccountGetDataAsync(int id)
        {
            return await _context.ChartOfAccounts.Include(c => c.ParentAccount).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ChartOfAccount>> ChartOfAccountGetAllDataAsync()
        {
            return await _context.ChartOfAccounts
            .Include(c => c.ParentAccount)
            .ToListAsync();
        }

        public async Task<bool> ExistsByAccountCodeAsync(string accountCode)
        {
            return await _context.ChartOfAccounts.AnyAsync(c => c.AccountCode == accountCode);
        }
    }
}
