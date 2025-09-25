﻿
namespace FinancialManagementSystem.Core
{
    public class User
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
