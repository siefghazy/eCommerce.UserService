using eCommerce.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Validatiors
{
    public class RegisterRequestValidators:AbstractValidator<RegisterRequestDto>
    {
       public RegisterRequestValidators()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is Required").EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is Required").MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.Gender).NotNull().IsInEnum().WithMessage("Invalid gender");

        }
    }
}
