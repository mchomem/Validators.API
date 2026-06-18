namespace DocBrNet.Core.Domain.Exceptions.CPF;

#pragma warning disable S101

public class CPFMaximumQuantityAllowedException : BusinessException
{
    public CPFMaximumQuantityAllowedException(int total)
        : base($"A quantidade máxima permitida para geração de CPF's é de {total}.") { }
}

#pragma warning restore S101