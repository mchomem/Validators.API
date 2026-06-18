namespace DocBrNet.Core.Domain.Interfaces;

public interface ICpf
{
    public IEnumerable<string> Generate(bool withMask, int maxGenerated);
}
