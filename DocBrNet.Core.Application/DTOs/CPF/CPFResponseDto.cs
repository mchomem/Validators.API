namespace DocBrNet.Core.Application.DTOs.CPF;

#pragma warning disable S101

public class CPFResponseDto
{
    public required string Value { get; init; }
    public required string MaskedValue { get; init; }
    public required bool IsValid { get; init; }
    public required int DefaultLength { get; init; }
}

#pragma warning restore S101
