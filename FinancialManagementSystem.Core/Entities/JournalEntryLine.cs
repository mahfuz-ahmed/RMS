

namespace FinancialManagementSystem.Core
{
    public class JournalEntryLine
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public string? LineDescription { get; set; }

        // Navigation properties
        public JournalEntry JournalEntry { get; set; } = null!;
        public ChartOfAccount Account { get; set; } = null!;
    }
}
