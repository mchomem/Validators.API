namespace DocBrNet.Core.Application.Interfaces;

public interface ICnpjService
{
    CnpjResponseDto Check(CnpjCheckerRequestDto cnpjRequest);

    IEnumerable<string> Generate(CnpjGeneratorRequestDto cnpjRequest);
}
