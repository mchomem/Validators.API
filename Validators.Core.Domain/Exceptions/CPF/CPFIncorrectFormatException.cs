namespace Validators.Core.Domain.Exceptions.CPF;

#pragma warning disable S101

public class CPFIncorrectFormatException : CPFException
{
    public CPFIncorrectFormatException(string message = "Formato incorreto para o CPF informado") : base(message) { }
}

#pragma warning restore S101
