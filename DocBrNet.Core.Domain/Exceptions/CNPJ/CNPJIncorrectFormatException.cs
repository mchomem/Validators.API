namespace DocBrNet.Core.Domain.Exceptions.CNPJ;

#pragma warning disable S101

public class CnpjIncorrectFormatException : CnpjException
{
    public CnpjIncorrectFormatException(string message = "Formato incorreto para o CNPJ informado.") : base(message) { }
}

#pragma warning restore S101