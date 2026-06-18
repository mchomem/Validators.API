namespace DocBrNet.Core.Domain.Exceptions.CPF;

public class CpfTooLongException : CpfException
{
    public CpfTooLongException(string message = "Valor do CPF é muito longo") : base(message){}
}
