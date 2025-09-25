using FluentValidation;

namespace FinancialManagementSystem.Application
{
    public class AddUSerCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUSerCommandValidator()
        {
            RuleFor(x => x.user.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.user.FullName).NotEmpty().WithMessage("FullName is required");
        }
    }
}
