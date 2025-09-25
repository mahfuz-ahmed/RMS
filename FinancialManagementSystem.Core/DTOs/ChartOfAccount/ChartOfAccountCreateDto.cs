
namespace FinancialManagementSystem.Core
{
    public class ChartOfAccountCreateDto
    {
        public string AccountCode { get; set; } 
        public string AccountName { get; set; } 
        public string AccountType { get; set; } 
        public int? ParentId { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
    }
}
