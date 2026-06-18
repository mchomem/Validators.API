namespace DocBrNet.Core.Application.Validators;

public class CnpjRequestValidator : AbstractValidator<CnpjRequestDto>
{
    public CnpjRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("CNPJ é obrigatório.")
            .Length(14, 18).WithMessage("CNPJ deve ter entre 14 e 18 caracteres (com ou sem máscara).");
    }
}
