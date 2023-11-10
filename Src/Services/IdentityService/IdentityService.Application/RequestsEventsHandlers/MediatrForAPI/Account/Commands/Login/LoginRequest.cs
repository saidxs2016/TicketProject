using FluentValidation;
using IdentityService.Application.DTO.ResultType;
using MediatR;

namespace IdentityService.Application.RequestsEventsHandlers.MediatrForAPI.Account.Commands.Login;


public class LoginRequest : IRequest<Result<LoginResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {

        RuleFor(p => p.Username)
            .NotNull().WithMessage("Kullanıcı adı veya şifre yanlış.")
            .NotEmpty().WithMessage("Kullanıcı adı veya şifre yanlış.")
            .MaximumLength(100).WithMessage("Kullanıcı adı veya şifre yanlış.");
        RuleFor(p => p.Password)
           .NotNull().WithMessage("Kullanıcı adı veya şifre yanlış.")
           .NotEmpty().WithMessage("Kullanıcı adı veya şifre yanlış.")
           .MaximumLength(50).WithMessage("Kullanıcı adı veya şifre yanlış.");

    }
}
