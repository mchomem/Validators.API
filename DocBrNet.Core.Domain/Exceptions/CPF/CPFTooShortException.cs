namespace DocBrNet.Core.Domain.Exceptions.CPF;

public class CpfTooShortException : CpfException
{
    public CpfTooShortException(string message = "Valor do CPF é muito curto") : base(message) { }
}
