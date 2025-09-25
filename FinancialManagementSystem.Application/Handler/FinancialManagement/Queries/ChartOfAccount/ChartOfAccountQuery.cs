using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public record ChartOfAccountGetDataQuery(int Id) : IRequest<ErrorOr<ChartOfAccountReadDto>>;
    public record ChartOfAccountGetAllDataQuery() : IRequest<ErrorOr<List<ChartOfAccountReadDto>>>;
}
