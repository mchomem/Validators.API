namespace DocBrNet.Core.Application.Validators;

/// <summary>
/// Validador para a requisição de CNPJ, garantindo que o valor seja obrigatório e tenha um comprimento adequado (entre 14 e 18 caracteres, considerando a possibilidade de máscara).
/// </summary>
public class CnpjCheckerRequestValidator : AbstractValidator<CnpjCheckerRequestDto>
{
    public CnpjCheckerRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("CNPJ é obrigatório.")
            .Length(14, 18).WithMessage("CNPJ deve ter entre 14 e 18 caracteres (com ou sem máscara).");
    }
}
