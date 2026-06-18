namespace DocBrNet.Core.Application.DTOs.CPF;

/// <summary>
/// DTO para requisição de CPF, contendo o valor do CPF a ser validado.
/// </summary>
public class CpfRequestDto
{
    /// <summary>
    /// Propriedade que representa o número do CPF a ser validado. Deve ser fornecida e pode conter entre 11 e 14 caracteres, dependendo se a máscara está presente ou não.
    /// </summary>
    public required string Value { get; init; }
}
