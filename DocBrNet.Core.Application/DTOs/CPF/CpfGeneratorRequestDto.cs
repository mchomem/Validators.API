namespace DocBrNet.Core.Application.DTOs.CPF;

/// <summary>
/// DTO para requisição de geração de CPF.
/// </summary>
public class CpfGeneratorRequestDto
{
    /// <summary>
    /// Propriedade que indica se o CNPJ gerado deve conter máscara ou não. O padrão é true, ou seja, com máscara.
    /// </summary>
    public bool WithMask { get; init; } = true;

    /// <summary>
    /// Propriedade que define a quantidade máxima de CNPJs a serem gerados. O valor padrão é 1.
    /// </summary>
    public int MaxGenerated { get; init; } = 1;
}
