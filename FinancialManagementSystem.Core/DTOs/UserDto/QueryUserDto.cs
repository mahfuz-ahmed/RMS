namespace FinancialManagementSystem.Core
{
    public class AuthUserDto
    {
        public int ID { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
        public string Role { get; set; } = default!;
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
