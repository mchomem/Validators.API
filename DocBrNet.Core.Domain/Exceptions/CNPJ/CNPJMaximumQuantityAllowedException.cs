namespace DocBrNet.Core.Domain.Exceptions.CNPJ;

#pragma warning disable S101

public class CNPJMaximumQuantityAllowedException : BusinessException
{
    public CNPJMaximumQuantityAllowedException(int total)
        : base($"A quantidade máxima permitida para geração de CNPJ's é de {total}.") { }
}

#pragma warning restore S101