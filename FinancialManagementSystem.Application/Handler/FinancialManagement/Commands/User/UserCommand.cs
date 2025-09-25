using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public record AddUserCommand(User user) : IRequest<ErrorOr<User>>;
    public record UpdateUserCommand(User user) : IRequest<ErrorOr<User>>;
    public record DeleteUserCommand(int userId) : IRequest<ErrorOr<bool>>;
}
