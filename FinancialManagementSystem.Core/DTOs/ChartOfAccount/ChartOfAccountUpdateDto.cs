
namespace FinancialManagementSystem.Core
{
    public class ChartOfAccountUpdateDto
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public int? ParentId { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public int? CreatedBy { get; set; }
        public string? ParentAccount{ get; set; }

    }
}
