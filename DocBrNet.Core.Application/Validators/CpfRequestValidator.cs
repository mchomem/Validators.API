namespace DocBrNet.Core.Application.Validators;

public class CpfRequestValidator : AbstractValidator<CpfRequestDto>
{
    public CpfRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Length(11, 14).WithMessage("CPF deve ter entre 11 e 14 caracteres (com ou sem máscara).");
    }
}
