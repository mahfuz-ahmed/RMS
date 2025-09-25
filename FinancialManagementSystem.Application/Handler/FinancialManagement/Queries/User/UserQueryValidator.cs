using FluentValidation;

namespace FinancialManagementSystem.Application
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<UserGetDataQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(item => item.id).Must(value => value is int intValue && intValue > 0)
                    .WithMessage("ID must be greater then zero")
                    .NotEmpty().WithMessage("ID cannot be empty.");
        }
    }
}
