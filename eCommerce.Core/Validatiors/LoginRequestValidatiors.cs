using eCommerce.Core.DTO;
using FluentValidation;


namespace eCommerce.Core.Validatiors
{
    public class LoginRequestValidatiors:AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidatiors()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }

    }
}
