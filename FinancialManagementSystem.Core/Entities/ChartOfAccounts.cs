
namespace FinancialManagementSystem.Core
{
    public class ChartOfAccount
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public int? ParentAccountId { get; set; } = null;
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? CreatedBy { get; set; }

        // Navigation property
        public ChartOfAccount? ParentAccount { get; set; }
        public ICollection<ChartOfAccount> ChildAccounts { get; set; } = new List<ChartOfAccount>();
        //public ICollection<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
    }
}
