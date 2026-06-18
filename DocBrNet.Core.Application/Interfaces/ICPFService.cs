namespace DocBrNet.Core.Application.Interfaces;

#pragma warning disable S101

public interface ICPFService
{
    CPFResponseDto Validate(string cpfEntrada);

    IEnumerable<string> Generate(bool withMask, int maxGenerated);
}

#pragma warning restore S101
