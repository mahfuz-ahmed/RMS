
namespace FinancialManagementSystem.Core
{
    public interface IJournalEntryLineRepository
    {
        Task<JournalEntryLine> AddJournalEntryLineAsync(JournalEntryLine jel);
        Task<JournalEntryLine> UpdateJournalEntryLineAsync(JournalEntryLine jel);
        Task<bool> DeleteJournalEntryLineAsync(JournalEntryLine jel);
        Task<JournalEntryLine> JournalEntryLineGetDataAsync(int id);
        Task<IEnumerable<JournalEntryLine>> JournalEntryLineGetAllDataAsync();
    }
}
