namespace Validators.Core.Application.Services;

#pragma warning disable S101

public class CNPJService : ICNPJService
{
    private readonly IMapper _mapper;

    public CNPJService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CNPJDto Validate(string cnpjEntrada)
    {
        var cnpj = new CNPJ(cnpjEntrada);
        cnpj.Validate();
        var cnpjDto = _mapper.Map<CNPJDto>(cnpj);
        return cnpjDto;
    }
}

#pragma warning restore S101
