namespace DocBrNet.Core.Application.Interfaces;

public interface ICnpjService
{
    CnpjResponseDto Check(CnpjRequestDto cnpjRequest);

    IEnumerable<string> Generate(TypeCnpj type, bool withMask, int maxGenerated);
}
