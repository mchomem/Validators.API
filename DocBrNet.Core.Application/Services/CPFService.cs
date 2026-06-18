namespace DocBrNet.Core.Application.Services;

public class CpfService : ICpfService
{
    private readonly IMapper _mapper;
    private readonly IValidator<CpfRequestDto> _validator;

    public CpfService(IMapper mapper, IValidator<CpfRequestDto> validator)
    {
        _mapper = mapper;
        _validator = validator;
    }

    public CpfResponseDto Validate(CpfRequestDto cpfRequest)
    {
        _validator.ValidateAndThrow(cpfRequest);

        var cpf = new Cpf(cpfRequest.Value);
        cpf.Validate();
        var cpfDto = _mapper.Map<CpfResponseDto>(cpf);
        return cpfDto;
    }

    public IEnumerable<string> Generate(bool withMask, int maxGenerated)
    {
        var cpf = new Cpf();
        return cpf.Generate(withMask, maxGenerated);
    }
}
