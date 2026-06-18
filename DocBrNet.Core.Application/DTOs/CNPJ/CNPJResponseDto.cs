namespace DocBrNet.Core.Application.DTOs.CNPJ;

public class CnpjResponseDto
{
    public required string Value { get; init; }
    public required string MaskedValue { get; init; }
    public required bool IsValid { get; init; }
    public required int DefaultLength { get; init; }
}
