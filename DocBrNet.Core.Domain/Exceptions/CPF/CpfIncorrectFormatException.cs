namespace DocBrNet.Core.Domain.Exceptions.CPF;

public class CpfIncorrectFormatException : CpfException
{
    public CpfIncorrectFormatException(string message = "Formato incorreto para o CPF informado") : base(message) { }
}
