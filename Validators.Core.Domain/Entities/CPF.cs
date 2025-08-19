namespace Validators.Core.Domain.Entities;

#pragma warning disable S101

public class CPF : IdentifierBase
{
    public CPF(string value) : base(value)
    {
        Value = value;
        MaskedValue = value;
        DefaultLength = 11;
    }

    public void Validate()
    {
        // Remover a máscara do CPF e espaços vazios, se existir.
        Value = Regex.Replace(Value, @"[.\- ]", string.Empty);

        if (Value.Length > DefaultLength)
            throw new CPFTooLongException($"Formato incorreto: o CPF deve conter {DefaultLength} dígitos.");

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
}

#pragma warning restore S101
