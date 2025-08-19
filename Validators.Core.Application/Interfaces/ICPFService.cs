namespace Validators.Core.Application.Interfaces;

#pragma warning disable S101

public interface ICPFService
{
    CPFDto Validate(string cpfEntrada);
}

#pragma warning restore S101
