namespace DocBrNet.Core.Application.Interfaces;

public interface ICnpjService
{
    CnpjResponseDto Check(CnpjRequestDto cnpjRequest);

    IEnumerable<string> Generate(TypeCNPJ type, bool withMask, int maxGenerated);
}
