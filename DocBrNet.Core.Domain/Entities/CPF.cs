namespace DocBrNet.Core.Domain.Entities;

/// <summary>
/// Representa um CPF (Cadastro de Pessoas Físicas), que é um identificador único para indivíduos no Brasil.
/// </summary>
public sealed class Cpf : IdentifierBase, ICpf
{
    public Cpf()
    {
        DefaultLength = 11;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe CPF com o valor fornecido. O construtor também define a máscara do CPF, caso ela não exista, e define o comprimento padrão para 11 dígitos.
    /// </summary>
    /// <param name="value">String representando o valor do CPF.</param>
    public Cpf(string value) : base(value)
    {
        Value = value;
        MaskedValue = SetDefaultMask(value);
        DefaultLength = 11;
    }

    /// <summary>
    /// Valida o CPF, removendo quaisquer caracteres de formatação (como pontos, traços e espaços) e verificando se o número resultante tem exatamente 11 dígitos. Em seguida, calcula os dígitos verificadores e compara com o valor fornecido para determinar se o CPF é válido ou não.
    /// </summary>
    /// <exception cref="CpfTooLongException">Ocorre quando o CPF informado é muito longo.</exception>
    /// <exception cref="CPFTooShortException">Ocorre quando o CPF informado é muito curto.</exception>
    public override void Check()
    {
        // Remove a máscara do CPF e espaços vazios, se existir.
        Value = Regex.Replace(Value, @"[.\- ]", string.Empty);

        if (Value.Length > DefaultLength)
            throw new CpfTooLongException($"Formato incorreto: o CPF deve conter {DefaultLength} dígitos.");

        if (Value.Length < DefaultLength)
            throw new CPFTooShortException($"Formato incorreto: o CPF deve conter {DefaultLength} dígitos.");

        // Previne caracteres repetidos, como "11111111111", "22222222222", etc.
        if (Value.All(c => c == Value.First()))
        {
            IsValid = false;
            return;
        }

        var cpfWithoutVD = Value.Substring(0, 9);
        var firstCheckDigit = CalculateCheckDigit(cpfWithoutVD);
        var secondCheckDigit = CalculateCheckDigit($"{cpfWithoutVD}{firstCheckDigit}");
        var calculatedCpf = $"{cpfWithoutVD}{firstCheckDigit}{secondCheckDigit}";

        IsValid = Value == calculatedCpf;
    }

    /// <summary>
    /// Gera uma lista de CPFs aleatórios, com a opção de incluir ou não a máscara padrão. O método aceita um parâmetro para definir a quantidade máxima de CPFs a serem gerados, garantindo que o número não ultrapasse um limite pré-definido para evitar sobrecarga do sistema.
    /// </summary>
    /// <param name="withMask">Indica se o CPF gerado deve incluir a máscara padrão.</param>
    /// <param name="maxGenerated">A quantidade máxima de CPFs a serem gerados.</param>
    /// <returns>Uma lista de CPFs gerados.</returns>
    /// <exception cref="CpfMaximumQuantityAllowedException">Ocorre quando a quantidade máxima de CPFs a serem gerados é excedida.</exception>
    public IEnumerable<string> Generate(bool withMask, int maxGenerated)
    {
        var maxValue = 100;
        var generatedCpfs = new List<string>();

        if (maxGenerated > maxValue)
        {
            throw new CpfMaximumQuantityAllowedException(maxValue);
        }

        for (int i = 0; i < maxGenerated; i++)
        {
            var random = new Random();
            var document = new StringBuilder();
            var documentGenerated = string.Empty;

            for (int j = 0; j < DefaultLength - 2; j++)
            {
                document.Append(random.Next(0, 10));
            }

            var firstCheckDigit = CalculateCheckDigit(document.ToString());
            var secondCheckDigit = CalculateCheckDigit($"{document.ToString()}{firstCheckDigit}");

            documentGenerated = $"{document.ToString()}{firstCheckDigit}{secondCheckDigit}";
            documentGenerated = withMask ? SetDefaultMask(documentGenerated) : documentGenerated;

            generatedCpfs.Add(documentGenerated);
        }
        
        return generatedCpfs;
    }

    /// <summary>
    /// Calcula o dígito verificador para um CPF. O cálculo é feito percorrendo os dígitos do CPF da direita para a esquerda, multiplicando cada dígito por um peso que aumenta a cada iteração. O resultado final é obtido através de uma operação de módulo 11.
    /// </summary>
    /// <param name="cpf">String representando o CPF sem os dígitos verificadores.</param>
    /// <returns>O dígito verificador calculado.</returns>
    public static int CalculateCheckDigit(string cpf)
    {
        // basead in https://www.cadcobol.com.br/calcula_cpf_cnpj_caepf.htm
        int _sum = 0;
        int initialWeight = cpf.Length + 1; // 10 para 1º DV, 11 para 2º DV

        for (int i = 0; i < cpf.Length; i++)
        {
            int digit = cpf[i] - '0'; // mais rápido que ToString()
            _sum += digit * initialWeight--;
        }

        int rest = (_sum * 10) % 11;
        return (rest == 10) ? 0 : rest;
    }

    /// <summary>
    /// Aplica a máscara padrão ao CPF, formatando-o no formato "XXX.XXX.XXX-XX". A função utiliza expressões regulares para inserir os pontos e o traço nos locais corretos, facilitando a leitura do CPF.
    /// </summary>
    /// <param name="cpfInput">String representando o CPF sem formatação.</param>
    /// <returns>String representando o CPF formatado.</returns>
    public static string SetDefaultMask(string cpfInput)
    {
        var result = Regex.Replace(cpfInput, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        return result;
    }
}
