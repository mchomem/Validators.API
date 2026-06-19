namespace DocBrNet.Core.Application.Services;

public class CpfService : ICpfService
{
    private readonly IMapper _mapper;
    private readonly IValidator<CpfCheckerRequestDto> _checkerValidator;
    private readonly IValidator<CpfGeneratorRequestDto> _generatorValidator;

    public CpfService(IMapper mapper, IValidator<CpfCheckerRequestDto> checkerValidator, IValidator<CpfGeneratorRequestDto> generatorValidator)
    {
        _mapper = mapper;
        _checkerValidator = checkerValidator;
        _generatorValidator = generatorValidator;
    }

    public CpfResponseDto Check(CpfCheckerRequestDto cpfRequest)
    {
        _checkerValidator.ValidateAndThrow(cpfRequest);

        var cpf = new Cpf(cpfRequest.Value);
        cpf.Check();
        var cpfDto = _mapper.Map<CpfResponseDto>(cpf);
        return cpfDto;
    }

    public IEnumerable<string> Generate(CpfGeneratorRequestDto cpfRequest)
    {
        _generatorValidator.ValidateAndThrow(cpfRequest);

        var cpf = new Cpf();
        return cpf.Generate(cpfRequest.WithMask, cpfRequest.MaxGenerated);
    }
}
