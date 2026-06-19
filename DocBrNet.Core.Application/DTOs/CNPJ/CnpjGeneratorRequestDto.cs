namespace DocBrNet.Core.Application.DTOs.CNPJ;

/// <summary>
/// DTO para requisição de geração de CNPJ.
/// </summary>
public class CnpjGeneratorRequestDto
{
    /// <summary>
    /// Propriedade que representa o tipo de CNPJ a ser gerado. Pode ser do tipo numérico ou alfanumérico.
    /// </summary>
    public TypeCnpj Type { get; init; } = TypeCnpj.Numeric;

    /// <summary>
    /// Propriedade que indica se o CNPJ gerado deve conter máscara ou não. O padrão é true, ou seja, com máscara.
    /// </summary>
    public bool WithMask { get; init; } = true;

    /// <summary>
    /// Propriedade que define a quantidade máxima de CNPJs a serem gerados. O valor padrão é 1.
    /// </summary>
    public int MaxGenerated { get; init; } = 1;
}
