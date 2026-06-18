namespace DocBrNet.Core.Domain.Exceptions.CPF;

#pragma warning disable S101

public class CPFTooShortException : CpfException
{
    public CPFTooShortException(string message = "Valor do CPF é muito curto") : base(message) { }
}

#pragma warning restore S101
