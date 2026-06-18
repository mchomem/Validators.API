namespace DocBrNet.Core.Application.DTOs.CPF;

/// <summary>
/// DTO para resposta de validação de CPF, contendo o valor do CPF, o valor mascarado, a validade e o comprimento padrão do CPF.
/// </summary>
public class CpfResponseDto
{
    /// <summary>
    /// Propriedade que representa o número do CPF original.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Propriedade que representa o número do CPF formatado com máscara.
    /// </summary>
    public required string MaskedValue { get; init; }

    /// <summary>
    /// Propriedade que indica se o CPF é válido.
    /// </summary>
    public required bool IsValid { get; init; }

    /// <summary>
    /// Propriedade que representa o comprimento padrão do número do CPF sem máscara.
    /// </summary>
    public required int DefaultLength { get; init; }
}
