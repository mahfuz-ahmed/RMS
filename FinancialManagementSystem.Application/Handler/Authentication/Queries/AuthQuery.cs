using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public record SignInQuery(string email, string password) : IRequest<AuthUserDto>;
}
