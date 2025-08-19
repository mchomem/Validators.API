namespace Validators.Core.Application.Interfaces;

#pragma warning disable S101

public interface ICNPJService
{
    CNPJDto Validate(string cnpjEntrada);
}

#pragma warning restore S101
