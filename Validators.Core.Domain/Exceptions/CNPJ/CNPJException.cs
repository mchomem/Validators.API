namespace Validators.Core.Domain.Exceptions.CNPJ;

#pragma warning disable S101

public class CNPJException : BusinessException
{
    public CNPJException(string message) : base(message) { }
}

#pragma warning restore S101
