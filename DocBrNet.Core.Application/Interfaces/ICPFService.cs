namespace DocBrNet.Core.Application.Interfaces;

public interface ICpfService
{
    CpfResponseDto Check(CpfCheckerRequestDto cpfRequest);

    IEnumerable<string> Generate(CpfGeneratorRequestDto cpfRequest);
}
