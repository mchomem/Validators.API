namespace DocBrNet.Core.Application.Validators;

/// <summary>
/// Validador para a requisição de CPF, garantindo que o valor seja obrigatório e tenha um comprimento adequado (entre 11 e 14 caracteres, considerando a possibilidade de máscara).
/// </summary>
public class CpfCheckerRequestValidator : AbstractValidator<CpfCheckerRequestDto>
{
    public CpfCheckerRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Length(11, 14).WithMessage("CPF deve ter entre 11 e 14 caracteres (com ou sem máscara).");
    }
}
