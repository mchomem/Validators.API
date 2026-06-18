namespace DocBrNet.Core.Application.Services;

public class CnpjService : ICnpjService
{
    private readonly IMapper _mapper;
    private readonly IValidator<CnpjRequestDto> _validator;

    public CnpjService(IMapper mapper, IValidator<CnpjRequestDto> validator)
    {
        _mapper = mapper;
        _validator = validator;
    }

    public CnpjResponseDto Check(CnpjRequestDto cnpjRequest)
    {
        _validator.ValidateAndThrow(cnpjRequest);

        var cnpj = new Cnpj(cnpjRequest.Value);
        cnpj.Validate();
        var cnpjDto = _mapper.Map<CnpjResponseDto>(cnpj);
        return cnpjDto;
    }

    public IEnumerable<string> Generate(TypeCnpj type, bool withMask, int maxGenerated)
    {
        var cnpj = new Cnpj();
        return cnpj.Generate(type, withMask, maxGenerated);
    }
}
