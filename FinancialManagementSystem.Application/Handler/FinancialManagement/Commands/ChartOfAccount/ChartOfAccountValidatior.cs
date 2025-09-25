using FluentValidation;

namespace FinancialManagementSystem.Application
{
    public class AddChartOfAccountCommandValidator: AbstractValidator<AddChartOfAccountCommand>
    {
        public AddChartOfAccountCommandValidator()
        {
            RuleFor(x => x.chartOfAccount.AccountCode)
                .NotEmpty().WithMessage("Account Code is required")
                .MaximumLength(50).WithMessage("Account Code cannot exceed 50 characters");

            RuleFor(x => x.chartOfAccount.AccountName)
                .NotEmpty().WithMessage("Account Name is required")
                .MaximumLength(200).WithMessage("Account Name cannot exceed 200 characters");

            RuleFor(x => x.chartOfAccount.AccountType)
                .NotEmpty().WithMessage("Account Type is required");

            // Example optional validation
            //RuleFor(x => x.chartOfAccount.ParentAccountId)
            //    .GreaterThanOrEqualTo(0).When(x => x.chartOfAccount.ParentAccountId.HasValue);
        }
    }
}
