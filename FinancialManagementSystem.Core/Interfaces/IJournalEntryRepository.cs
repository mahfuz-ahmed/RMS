
namespace FinancialManagementSystem.Core
{
    public interface IJournalEntryRepository
    {
        Task<JournalEntry> AddJournalEntryAsync(JournalEntry je);
        Task<JournalEntry> UpdateJournalEntryAsync(JournalEntry je);
        Task<bool> DeleteJournalEntryAsync(JournalEntry je);
        Task<JournalEntry> JournalEntryGetDataAsync(int id);
        Task<IEnumerable<JournalEntry>> JournalEntryGetAllDataAsync();
    }
}
