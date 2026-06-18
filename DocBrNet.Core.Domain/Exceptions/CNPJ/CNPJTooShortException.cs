namespace DocBrNet.Core.Domain.Exceptions.CNPJ;

public class CnpjTooShortException : CnpjException
{
    public CnpjTooShortException(string message = "Valor do CNPJ é muito curto") : base(message) { }
}
