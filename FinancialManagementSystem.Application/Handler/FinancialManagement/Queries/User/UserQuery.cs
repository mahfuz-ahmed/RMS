using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public record UserGetDataQuery(int id) : IRequest<ErrorOr<User>>;
    public record UserGetAllDataQuery() : IRequest<ErrorOr<IEnumerable<User>>>;
}
