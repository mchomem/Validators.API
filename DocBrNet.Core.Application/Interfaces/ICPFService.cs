namespace DocBrNet.Core.Application.Interfaces;

#pragma warning disable S101

public interface ICPFService
{
    CPFResponseDto Validate(string cpfEntrada);
}

#pragma warning restore S101
