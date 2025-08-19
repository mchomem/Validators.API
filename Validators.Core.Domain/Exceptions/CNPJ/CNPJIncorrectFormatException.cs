namespace Validators.Core.Domain.Exceptions.CNPJ;

#pragma warning disable S101

public class CNPJIncorrectFormatException : CNPJException
{
    public CNPJIncorrectFormatException(string message = "Formato incorreto para o CNPJ informado.") : base(message) { }
}

#pragma warning restore S101