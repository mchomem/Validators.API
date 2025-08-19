namespace Validators.Core.Application.DTOs;

#pragma warning disable S101

public class CNPJDto
{
    public string Value { get; set; }
    public string MaskedValue { get; set; }
    public bool IsValid { get; set; }
    public int DefaultLength { get; set; }
}

#pragma warning restore S101
