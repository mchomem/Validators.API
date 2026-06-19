namespace DocBrNet.Core.Application.Validators;

/// <summary>
/// Validador para a solicitação de geração de CPFs. Este validador garante que o número máximo de CPFs a serem gerados seja maior que zero e que a propriedade 'WithMask' seja informada.
/// </summary>
public class CpfGeneratorRequestValidator : AbstractValidator<CpfGeneratorRequestDto>
{
    public CpfGeneratorRequestValidator()
    {
        RuleFor(x => x.MaxGenerated)
            .GreaterThan(0).WithMessage("O número máximo de CPFs a serem gerados deve ser maior que zero.");

        RuleFor(x => x.WithMask)
            .NotNull().WithMessage("A propriedade 'WithMask' deve ser informada.");
    }
}
