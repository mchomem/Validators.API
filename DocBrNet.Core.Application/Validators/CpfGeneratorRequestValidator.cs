namespace DocBrNet.Core.Application.Validators;

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
