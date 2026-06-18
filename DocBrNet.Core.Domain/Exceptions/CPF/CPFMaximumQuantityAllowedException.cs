namespace DocBrNet.Core.Domain.Exceptions.CPF;

public class CpfMaximumQuantityAllowedException : BusinessException
{
    public CpfMaximumQuantityAllowedException(int total)
        : base($"A quantidade máxima permitida para geração de CPF's é de {total}.") { }
}
