

namespace FinancialManagementSystem.Core
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public string JournalNumber { get; set; } = null!;
        public DateTime EntryDate { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        // Navigation property
        public ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}
