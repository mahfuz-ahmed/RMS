
namespace FinancialManagementSystem.Core
{
    public interface IChartOfAccountRepository
    {
        Task<ChartOfAccount> AddChartOfAccountAsync(ChartOfAccount coa);
        Task<ChartOfAccount> UpdateChartOfAccountAsync(ChartOfAccount coa);
        Task<bool> DeleteUserAsync(ChartOfAccount coa);
        Task<ChartOfAccount> ChartOfAccountGetDataAsync(int id);
        Task<bool> ExistsByAccountCodeAsync(string accountCode);
        Task<IEnumerable<ChartOfAccount>> ChartOfAccountGetAllDataAsync();
    }
}
