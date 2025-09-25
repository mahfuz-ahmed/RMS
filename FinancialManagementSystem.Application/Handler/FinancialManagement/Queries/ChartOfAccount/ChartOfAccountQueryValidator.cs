using FluentValidation;

namespace FinancialManagementSystem.Application.ChartOfAccounts
{
    public class ChartOfAccountGetDataQueryValidator: AbstractValidator<ChartOfAccountGetDataQuery>
    {
        public ChartOfAccountGetDataQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ChartOfAccount ID must be greater than zero.");
        }
    }
}
