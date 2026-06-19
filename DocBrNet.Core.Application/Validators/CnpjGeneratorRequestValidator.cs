namespace DocBrNet.Core.Application.Validators;

/// <summary>
/// Validador para as requisições de geração de CNPJ, garantindo que os dados fornecidos sejam válidos antes de processar a solicitação.
/// </summary>
public class CnpjGeneratorRequestValidator : AbstractValidator<CnpjGeneratorRequestDto>
{
    public CnpjGeneratorRequestValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("O tipo de CNPJ deve ser 'Numeric' ou 'Alphanumeric'.");

        RuleFor(x => x.MaxGenerated)
            .GreaterThan(0).WithMessage("O número máximo de CNPJs a serem gerados deve ser maior que zero.");

        RuleFor(x => x.WithMask)
            .NotNull().WithMessage("A propriedade 'WithMask' deve ser informada.");
    }
}
