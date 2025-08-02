using CentralTask.Core.Mediator.Commands;
using FluentValidation;

namespace CentralTask.Application.Commands.UserCommands.CriarUserCommand;

public class CriarUserCommandInputValidator : CommandInputValidator<CriarUserCommandInput>
{
    public CriarUserCommandInputValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(300);

        RuleFor(x => x.Senha)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(36);

        RuleFor(x => x.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(250);
    }
}