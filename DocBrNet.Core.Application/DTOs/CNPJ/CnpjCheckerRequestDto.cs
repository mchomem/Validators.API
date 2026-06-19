namespace DocBrNet.Core.Application.DTOs.CNPJ;

/// <summary>
/// DTO para requisição de verificação de CNPJ.
/// </summary>
public class CnpjCheckerRequestDto
{
    /// <summary>
    /// Propriedade que representa o número do CNPJ a ser verificado. Deve ser fornecida e pode conter entre 14 e 18 caracteres, dependendo se a máscara está presente ou não.
    /// </summary>
    public required string Value { get; init; }
}
