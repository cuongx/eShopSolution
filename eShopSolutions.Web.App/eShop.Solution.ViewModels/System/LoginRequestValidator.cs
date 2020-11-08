
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.System
{
    public class LoginRequestValidator : AbstractValidator<LoginResquest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");
        }
    }
}
