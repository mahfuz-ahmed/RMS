using ErrorOr;
using FinancialManagementSystem.Core;
using MediatR;

namespace FinancialManagementSystem.Application
{
    public record AddChartOfAccountCommand(ChartOfAccountCreateDto chartOfAccount) : IRequest<ErrorOr<ChartOfAccountReadDto>>;
    public record UpdateChartOfAccountCommand(ChartOfAccountUpdateDto chartOfAccount) : IRequest<ErrorOr<ChartOfAccountReadDto>>;
    public record DeleteChartOfAccountCommand(int chartOfAccountId) : IRequest<ErrorOr<bool>>;
}
