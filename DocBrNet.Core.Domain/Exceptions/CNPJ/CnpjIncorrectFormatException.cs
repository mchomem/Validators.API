namespace DocBrNet.Core.Domain.Exceptions.CNPJ;

public class CnpjIncorrectFormatException : CnpjException
{
    public CnpjIncorrectFormatException(string message = "Formato incorreto para o CNPJ informado.") : base(message) { }
}
