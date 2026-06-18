namespace DocBrNet.Core.Domain.Interfaces;

public interface ICnpj
{
    public IEnumerable<string> Generate(TypeCnpj type, bool withMask, int maxGenerated);
}
