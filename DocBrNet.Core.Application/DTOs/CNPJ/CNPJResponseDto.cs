namespace DocBrNet.Core.Application.DTOs.CNPJ;

/// <summary>
/// DTO para resposta de validação de CNPJ, contendo o valor original, o valor formatado (com máscara), a validade do CNPJ e o comprimento padrão do número sem máscara.
/// </summary>
public class CnpjResponseDto
{
    /// <summary>
    /// Propriedade que representa o número do CNPJ original.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Propriedade que representa o número do CNPJ formatado com máscara.
    /// </summary>
    public required string MaskedValue { get; init; }

    /// <summary>
    /// Propriedade que indica se o CNPJ é válido.
    /// </summary>
    public required bool IsValid { get; init; }

    /// <summary>
    /// Propriedade que representa o comprimento padrão do número do CNPJ sem máscara.
    /// </summary>
    public required int DefaultLength { get; init; }
}
