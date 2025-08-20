namespace Validators.UnitTest;

public class CPFUnitTest
{
    [Theory]
    [InlineData("280.012.389-38")]
    [InlineData("123.456.789-09")]  
    [InlineData("987.654.321-00")]  
    [InlineData("111.444.777-35")]  
    [InlineData("101.150.990-34")]
    public void Validate_MaskedValue_ReturnTrue(string cpfValue)
    {
        // Arrange
        var cpf = new CPF(cpfValue);

        // Act
        cpf.Validate();

        // Asert
        Assert.True(cpf.IsValid);
    }

    [Theory]
    [InlineData("28001238938")]
    [InlineData("22488465097")]
    [InlineData("42628943042")]
    [InlineData("59338512053")]
    [InlineData("66974406002")]
    public void Validate_NonMaskedValue_ReturnTrue(string cpfValue)
    {
        // Arrange
        var cpf = new CPF(cpfValue);

        // Act
        cpf.Validate();

        // Asert
        Assert.True(cpf.IsValid);
    }

    [Theory]
    [InlineData("111.111.111-11")]
    [InlineData("222.222.222-22")]
    [InlineData("333.333.333-33")]
    [InlineData("444.444.444-44")]
    [InlineData("555.555.555-55")]
    public void Validate_Value_ReturnFalse(string cpfValue)
    {
        // Arrange
        var cpf = new CPF(cpfValue);

        // Act
        cpf.Validate();

        // Asert
        Assert.False(cpf.IsValid);
    }

    [Theory]
    [InlineData("0")]
    public void Validate_ShortCPF_ThrowsCPFTooShortException(string cpfValue)
    {
        // Arrange
        var cpf = new CPF(cpfValue);

        // Assert & Act
        Assert.Throws<CPFTooShortException>(() =>
        {
            cpf.Validate();
        });
    }

    [Theory]
    [InlineData("123456789123456789")]
    public void Validate_LongCPF_ThrowsCPFTooLongException(string cpfValue)
    {
        // Arrange
        var cpf = new CPF(cpfValue);

        // Assert & Act
        Assert.Throws<CPFTooLongException>(() =>
        {
            cpf.Validate();
        });
    }
}
