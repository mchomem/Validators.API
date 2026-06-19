namespace DocBrNet.Core.Application.DTOs.CPF;

/// <summary>
/// DTO para requisição de CPF, contendo o valor do CPF a ser verificado.
/// </summary>
public class CpfCheckerRequestDto
{
    /// <summary>
    /// Propriedade que representa o número do CPF a ser verificado. Deve ser fornecida e pode conter entre 11 e 14 caracteres, dependendo se a máscara está presente ou não.
    /// </summary>
    public required string Value { get; init; }
}
