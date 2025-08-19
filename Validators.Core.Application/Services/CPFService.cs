namespace Validators.Core.Application.Services;

#pragma warning disable S101

public class CPFService : ICPFService
{
    private readonly IMapper _mapper;

    public CPFService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CPFDto Validate(string cpfEntrada)
    {
        var cpf = new CPF(cpfEntrada);
        cpf.Validate();
        var cpfDto = _mapper.Map<CPFDto>(cpf);
        return cpfDto;
    }
}

#pragma warning restore S101
