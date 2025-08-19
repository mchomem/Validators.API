namespace Validators.Core.Domain.Exceptions.CPF;

#pragma warning disable S101

public class CPFException : BusinessException
{
    public CPFException(string message) : base(message) { }
}

#pragma warning restore S101
