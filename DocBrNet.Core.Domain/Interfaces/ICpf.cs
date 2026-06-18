namespace DocBrNet.Core.Domain.Interfaces;

public interface ICpf
{
    public string Generate(bool withMask);
}
