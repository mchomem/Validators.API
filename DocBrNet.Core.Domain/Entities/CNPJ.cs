namespace DocBrNet.Core.Domain.Entities;

/// <summary>
/// Representa um CNPJ (Cadastro Nacional da Pessoa Jurídica), que é um identificador único para empresas no Brasil.
/// </summary>
public sealed class Cnpj : IdentifierBase, ICnpj
{
    public Cnpj()
    {
        DefaultLength = 14;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe CNPJ com o valor fornecido. O construtor também define a máscara do CNPJ, caso ela não exista,
    /// e define o comprimento padrão para 14 dígitos.
    /// </summary>
    /// <param name="value">String representando o valor do CNPJ.</param>
    public Cnpj(string value) : base(value)
    {
        Value = value;
        MaskedValue = SetDefaultMask(value);
        DefaultLength = 14;
    }

    /// <summary>
    /// Valida o valor do CNPJ, removendo quaisquer caracteres de formatação (como pontos, barras e hífens) e verificando se o comprimento é correto.
    /// Em seguida, calcula os dígitos verificadores e compara com os dígitos fornecidos para determinar se o CNPJ é válido.
    /// </summary>
    /// <exception cref="CnpjTooLongException">Ocorre quando o CNPJ informado é muito longo.</exception>
    /// <exception cref="CnpjTooShortException">Ocorre quando o CNPJ informado é muito curto.</exception>
    public override void Validate()
    {
        // Remover a máscara do CNPJ e espaços vazios, se existir.
        Value = Regex.Replace(Value, @"[./\- ]", string.Empty);

        var thereAnyLetters = Value.Any(char.IsLetter);

        if (Value.Length > DefaultLength)
            throw new CnpjTooLongException($"O CNPJ informado é muito longo. O tamanho padrão é {DefaultLength} dígitos {(thereAnyLetters ? "alfanuméricos" : "numérico")}.");

        if (Value.Length < DefaultLength)
            throw new CnpjTooShortException($"O CNPJ informado é muito curto. O tamanho padrão é {DefaultLength} dígitos {(thereAnyLetters ? "alfanuméricos" : "numérico")}.");

        if(Value.All(c => c == Value.First()))
        {
            IsValid = false;
            return;
        }

        var cnpjWithoutVD = Value.Substring(0, Value.Length - 2);
        var firstCheckDigit = 0;
        var secondCheckDigit = 0;
        var calculatedCnpj = string.Empty;

        // Verificar se o CNPJ contém apenas números ou letras.
        if (thereAnyLetters)
        {
            firstCheckDigit = CalculateCheckDigitAlphanumeric(cnpjWithoutVD);
            secondCheckDigit = CalculateCheckDigitAlphanumeric($"{cnpjWithoutVD}{firstCheckDigit}");
        }
        else
        {
            firstCheckDigit = CalculateCheckDigitNumeric(cnpjWithoutVD);
            secondCheckDigit = CalculateCheckDigitNumeric($"{cnpjWithoutVD}{firstCheckDigit}");
        }

        calculatedCnpj = $"{cnpjWithoutVD}{firstCheckDigit}{secondCheckDigit}";
        IsValid = Value == calculatedCnpj;
    }

    public IEnumerable<string> Generate(TypeCnpj type, bool withMask, int maxGenerated)
    {
        var maxValue = 100;
        var generatedCnpjs = new List<string>();

        if (maxGenerated > maxValue)
        {
            throw new CnpjMaximumQuantityAllowedException(maxValue);
        }

        for (int i = 0; i < maxGenerated; i++)
        {
            var random = new Random();
            var seletor = new Random();
            var document = new StringBuilder();
            var firstCheckDigit = 0;
            var secondCheckDigit = 0;
            var documentGenerated = string.Empty;

            if (type == TypeCnpj.Numeric)
            {
                for (int j = 0; j < DefaultLength - 2; j++)
                {
                    document.Append(random.Next(0, 10));
                }

                firstCheckDigit = CalculateCheckDigitNumeric(document.ToString());
                secondCheckDigit = CalculateCheckDigitNumeric($"{document.ToString()}{firstCheckDigit}");
            }
            else
            {
                for (int j = 0; j < DefaultLength - 2; j++)
                {
                    if (i >= 0 && i <= 7)
                        // Seleciona aleatoriamente entre gerar um dígito numérico ou uma letra maiúscula para os primeiros 8 caracteres do CNPJ.
                        if (seletor.Next(0, 10) >= 3)
                            document.Append((char)random.Next('A', 'Z' + 1));
                        else
                            document.Append(random.Next(0, 10));
                    else if (j >= 8 && j <= 10)
                        document.Append("0");
                    else if (j == 11)
                        document.Append("1");
                    else
                        document.Append(random.Next(0, 10));
                }

                firstCheckDigit = CalculateCheckDigitAlphanumeric(document.ToString());
                secondCheckDigit = CalculateCheckDigitAlphanumeric($"{document.ToString()}{firstCheckDigit}");
            }

            documentGenerated = $"{document.ToString()}{firstCheckDigit}{secondCheckDigit}";
            documentGenerated = withMask ? SetDefaultMask(documentGenerated) : documentGenerated;

            generatedCnpjs.Add(documentGenerated);
        }

        return generatedCnpjs;
    }

    /// <summary>
    /// Calcula o dígito verificador para um CNPJ alfanumérico. O cálculo é feito percorrendo os dígitos
    /// do CNPJ da direita para a esquerda, multiplicando cada dígito por um peso que aumenta a cada iteração.
    /// O resultado final é obtido através de uma operação de módulo 11.
    /// </summary>
    /// <param name="cnpj">String representando o CNPJ sem os dígitos verificadores.</param>
    /// <returns>O dígito verificador calculado.</returns>
    private static int CalculateCheckDigitAlphanumeric(string cnpj)
    {
        var weight = 2;
        var sum = 0;

        // Percorre os dígitos do CNPJ da direita para a esquerda (de trás pra frente).
        for (int indice = cnpj.Length - 1; indice >= 0; indice--)
        {
            char character = cnpj[indice];
            int value = GetValueToDigit(character);
            int product = value * weight;
            sum += product;
            weight++;

            // Reinicia o peso após o 8º dígito para o primeiro dígito.
            if (indice == 4 && cnpj.Length == 12)
                weight = 2;

            // Reinicia o peso após o 8º dígito para o segundo dígito.
            if (indice == 5 && cnpj.Length == 13)
                weight = 2;
        }

        int rest = sum % 11;
        int checkDigit = 0;

        if (rest == 1 || rest == 0)
            checkDigit = 0;
        else
            checkDigit = 11 - rest;

        return checkDigit;
    }

    /// <summary>
    /// Calcula o dígito verificador para um CNPJ numérico. O cálculo é feito percorrendo os dígitos do CNPJ da direita para a esquerda, multiplicando cada dígito por um peso que varia de 2 a 9.
    /// O resultado final é obtido através de uma operação de módulo 11.
    /// </summary>
    /// <param name="cnpj">String representando o CNPJ sem os dígitos verificadores.</param>
    /// <returns>O dígito verificador calculado.</returns>
    private static int CalculateCheckDigitNumeric(string cnpj)
    {
        // A validação do CNPJ usa pesos que variam de 2 a 9.
        // O array de pesos é definido para ser percorrido da direita para a esquerda.
        int[] weights = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum = 0;

        // A lógica de cálculo do dígito verificador para o CNPJ é feita da direita para a esquerda.
        // Vamos iterar sobre os 12 primeiros dígitos, que incluem o CNPJ base e a filial.
        for (int i = 0; i < cnpj.Length; i++)
        {
            // Converte o caractere para um número inteiro.
            int numericDigit = int.Parse(cnpj[i].ToString());

            // Multiplica o dígito pelo peso correspondente e adiciona à soma.
            // O peso a ser usado depende da quantidade de dígitos do CNPJ.
            if (cnpj.Length == 12)
            {
                sum += numericDigit * weights[i];
            }
            else if (cnpj.Length == 13)
            {
                // Para o segundo dígito verificador, o CNPJ tem 13 dígitos.
                // Os pesos mudam para incluir o primeiro dígito verificador.
                int[] weights2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                sum += numericDigit * weights2[i];
            }
        }

        // Calcula o resto da divisão da soma por 11.
        int checkDigit = sum % 11;

        // Se o resto for 0 ou 1, o dígito verificador é 0. Caso contrário, é 11 menos o resto.
        if (checkDigit < 2)
            return 0;

        return 11 - checkDigit;
    }

    /// <summary>
    /// Obtém o valor numérico correspondente a um caractere alfanumérico. Para caracteres de '0' a '9', retorna o valor numérico correspondente.
    /// Para caracteres de 'A' a 'Z', retorna um valor numérico que começa em 17 para 'A' e aumenta em 1 para cada letra subsequente.
    /// </summary>
    /// <param name="character">O caractere alfanumérico a ser convertido em valor numérico.</param>
    /// <returns>O valor numérico correspondente ao caractere.</returns>
    private static int GetValueToDigit(char character)
    {
        var number = 0;

        switch (character)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                number = int.Parse(character.ToString());
                break;

            case 'A': number = 17; break;
            case 'B': number = 18; break;
            case 'C': number = 19; break;
            case 'D': number = 20; break;
            case 'E': number = 21; break;
            case 'F': number = 22; break;
            case 'G': number = 23; break;
            case 'H': number = 24; break;
            case 'I': number = 25; break;
            case 'J': number = 26; break;
            case 'K': number = 27; break;
            case 'L': number = 28; break;
            case 'M': number = 29; break;
            case 'N': number = 30; break;
            case 'O': number = 31; break;
            case 'P': number = 32; break;
            case 'Q': number = 33; break;
            case 'R': number = 34; break;
            case 'S': number = 35; break;
            case 'T': number = 36; break;
            case 'U': number = 37; break;
            case 'V': number = 38; break;
            case 'W': number = 39; break;
            case 'X': number = 40; break;
            case 'Y': number = 41; break;
            case 'Z': number = 42; break;
        }

        return number;
    }

    /// <summary>
    /// Aplica a máscara padrão de CNPJ ao valor fornecido, caso ele ainda não possua a formatação correta. A máscara padrão é "XX.XXX.XXX/XXXX-XX", onde "X" representa um dígito do CNPJ.
    /// </summary>
    /// <param name="cnpjInput">O valor do CNPJ a ser formatado.</param>
    /// <returns>O CNPJ formatado com a máscara padrão.</returns>
    private static string SetDefaultMask(string cnpjInput)
    {
        var result = Regex.Replace(cnpjInput, @"^(.{2})(.{3})(.{3})(.{4})(.{2})$", "$1.$2.$3/$4-$5");
        return result;
    }
}
