namespace Validators.Core.Domain.Exceptions.CNPJ;

#pragma warning disable S101

public class CNPJTooShortException : CNPJException
{
    public CNPJTooShortException(string message = "Valor do CNPJ é muito curto") : base(message) { }
}

#pragma warning restore S101