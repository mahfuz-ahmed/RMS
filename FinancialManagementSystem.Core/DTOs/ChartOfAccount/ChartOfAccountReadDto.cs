namespace FinancialManagementSystem.Core
{
    public class ChartOfAccountReadDto
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; } 
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public string? ParentAccountName { get; set; }
    }
}
