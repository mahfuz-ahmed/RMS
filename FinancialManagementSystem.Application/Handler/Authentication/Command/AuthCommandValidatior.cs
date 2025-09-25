using FluentValidation;

namespace FinancialManagementSystem.Application
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.user.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.user.PasswordHash).NotEmpty().WithMessage("Password is required");
        }
    }
}
