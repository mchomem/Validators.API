namespace Validators.Core.Domain.Exceptions.CNPJ;

#pragma warning disable S101

public class CNPJTooLongException : CNPJException
{
    public CNPJTooLongException(string message = "Valor do CNPJ é muito longo") : base(message) { }
}

#pragma warning restore S101
