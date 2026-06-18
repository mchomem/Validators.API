namespace DocBrNet.Core.Domain.Interfaces;

public interface ICnpj
{
    public string Generate(TypeCNPJ type, bool withMask);
}
