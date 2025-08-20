namespace Validators.Core.Domain.Entities.Bases;

public abstract class IdentifierBase
{
    protected IdentifierBase(string value)
    {
        Value = value;
    }

    public string Value { get; protected set; }
    public string MaskedValue { get; protected set; } = string.Empty;
    public bool IsValid { get; protected set; }
    public int DefaultLength { get; protected set; }

    public abstract void Validate();
}
