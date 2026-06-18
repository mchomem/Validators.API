namespace DocBrNet.Core.Application.DTOs.CNPJ;

/// <summary>
/// DTO para requisição de validação de CNPJ.
/// </summary>
public class CnpjRequestDto
{
    /// <summary>
    /// Propriedade que representa o número do CNPJ a ser validado. Deve ser fornecida e pode conter entre 14 e 18 caracteres, dependendo se a máscara está presente ou não.
    /// </summary>
    public required string Value { get; init; }
}
