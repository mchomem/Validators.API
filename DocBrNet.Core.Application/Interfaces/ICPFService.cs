namespace DocBrNet.Core.Application.Interfaces;

public interface ICpfService
{
    CpfResponseDto Validate(CpfRequestDto cpfRequest);

    IEnumerable<string> Generate(bool withMask, int maxGenerated);
}
