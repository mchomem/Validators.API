namespace DocBrNet.Core.Application.Services;

#pragma warning disable S101

public class CNPJService : ICNPJService
{
    private readonly IMapper _mapper;

    public CNPJService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CNPJResponseDto Validate(string cnpjEntrada)
    {
        var cnpj = new CNPJ(cnpjEntrada);
        cnpj.Validate();
        var cnpjDto = _mapper.Map<CNPJResponseDto>(cnpj);
        return cnpjDto;
    }

    public IEnumerable<string> Generate(TypeCNPJ type, bool withMask, int maxGenerated)
    {
        var cnpj = new CNPJ();
        var cnpjs = new List<string>();
        var maxValue = 100;

        if (maxGenerated > maxValue)
        {
            throw new CNPJMaximumQuantityAllowedException(maxValue);
        }

        for (int i = 0; i < maxGenerated; i++)
        {
            cnpjs.Add(cnpj.Generate(type, withMask));
        }

        return cnpjs;
    }
}

#pragma warning restore S101
