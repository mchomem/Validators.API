namespace DocBrNet.Core.Domain.Exceptions.CNPJ;

public class CnpjMaximumQuantityAllowedException : BusinessException
{
    public CnpjMaximumQuantityAllowedException(int total)
        : base($"A quantidade máxima permitida para geração de CNPJ's é de {total}.") { }
}
