namespace DocBrNet.Core.Domain.Interfaces;

public interface ICnpj
{
    public IEnumerable<string> Generate(TypeCNPJ type, bool withMask, int maxGenerated);
}
