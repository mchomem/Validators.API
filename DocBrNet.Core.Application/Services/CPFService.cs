namespace DocBrNet.Core.Application.Services;

#pragma warning disable S101

public class CPFService : ICPFService
{
    private readonly IMapper _mapper;

    public CPFService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CPFResponseDto Validate(string cpfEntrada)
    {
        var cpf = new CPF(cpfEntrada);
        cpf.Validate();
        var cpfDto = _mapper.Map<CPFResponseDto>(cpf);
        return cpfDto;
    }

    public IEnumerable<string> Generate(bool withMask, int maxGenerated)
    {
        var cpf = new CPF();
        var generatedCpfs = new List<string>();
        var maxValue = 100;

        if(maxGenerated > maxValue)
        {
            throw new CPFMaximumQuantityAllowedException(maxValue);
        }

        for (int i = 0; i < maxGenerated; i++)
        {
            
            generatedCpfs.Add(cpf.Generate(withMask));
        }
        return generatedCpfs;
    }
}

#pragma warning restore S101
