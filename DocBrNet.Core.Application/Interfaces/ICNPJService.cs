namespace DocBrNet.Core.Application.Interfaces;

#pragma warning disable S101

public interface ICNPJService
{
    CNPJResponseDto Validate(string cnpjEntrada);

    IEnumerable<string> Generate(TypeCNPJ type, bool withMask, int maxGenerated);
}

#pragma warning restore S101
