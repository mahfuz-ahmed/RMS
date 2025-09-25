using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public record SignUpCommand(User user) : IRequest<User>;
    public record RefreshTokenCommand(string RefreshToken) : IRequest<(string JwtToken, string RefreshToken)>;
}
