namespace Validators.Core.Domain.Exceptions.CPF;

#pragma warning disable S101

public class CPFTooLongException : CPFException
{
    public CPFTooLongException(string message = "Valor do CPF é muito longo") : base(message){}
}

#pragma warning restore S101
