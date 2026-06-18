namespace DocBrNet.Core.Domain.Exceptions.CNPJ;

public class CnpjTooLongException : CnpjException
{
    public CnpjTooLongException(string message = "Valor do CNPJ é muito longo") : base(message) { }
}
