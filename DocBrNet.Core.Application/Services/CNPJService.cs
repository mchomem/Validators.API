namespace DocBrNet.Core.Application.Services;

public class CnpjService : ICnpjService
{
    private readonly IMapper _mapper;
    private readonly IValidator<CnpjCheckerRequestDto> _checkerValidator;
    private readonly IValidator<CnpjGeneratorRequestDto> _generatorValidator;

    public CnpjService(IMapper mapper, IValidator<CnpjCheckerRequestDto> checkerValidator, IValidator<CnpjGeneratorRequestDto> generatorValidator)
    {
        _mapper = mapper;
        _checkerValidator = checkerValidator;
        _generatorValidator = generatorValidator;
    }

    public CnpjResponseDto Check(CnpjCheckerRequestDto cnpjRequest)
    {
        var result = _checkerValidator.Validate(cnpjRequest);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage);
            throw new ValidationException(string.Join("; ", errors));
        }

        var cnpj = new Cnpj(cnpjRequest.Value);
        cnpj.Check();
        var cnpjDto = _mapper.Map<CnpjResponseDto>(cnpj);
        return cnpjDto;
    }

    public IEnumerable<string> Generate(CnpjGeneratorRequestDto cnpjRequest)
    {
        var result = _generatorValidator.Validate(cnpjRequest);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage);
            throw new ValidationException(string.Join("; ", errors));
        }

        var cnpj = new Cnpj();
        return cnpj.Generate(cnpjRequest.Type, cnpjRequest.WithMask, cnpjRequest.MaxGenerated);
    }
}
