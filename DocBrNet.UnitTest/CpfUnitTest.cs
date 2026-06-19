namespace DocBrNet.UnitTest;

public class CpfUnitTest
{
    [Theory]
    [InlineData("280.012.389-38")]
    [InlineData("123.456.789-09")]  
    [InlineData("987.654.321-00")]  
    [InlineData("111.444.777-35")]  
    [InlineData("101.150.990-34")]
    public void Check_MaskedValue_ReturnTrue(string cpfValue)
    {
        AssertValidCPF(cpfValue);
    }

    [Theory]
    [InlineData("28001238938")]
    [InlineData("22488465097")]
    [InlineData("42628943042")]
    [InlineData("59338512053")]
    [InlineData("66974406002")]
    public void Check_NonMaskedValue_ReturnTrue(string cpfValue)
    {
        AssertValidCPF(cpfValue);
    }

    [Theory]
    [InlineData("111.111.111-11")]
    [InlineData("222.222.222-22")]
    [InlineData("333.333.333-33")]
    [InlineData("444.444.444-44")]
    [InlineData("555.555.555-55")]
    public void Check_WithSameNumericalValue_ReturnFalse(string cpfValue)
    {
        AssertInvalidCPF(cpfValue);
    }

    [Theory]
    [InlineData("321.323.658-24")]
    [InlineData("255.130.987-52")]
    [InlineData("152.302.158-68")]
    [InlineData("352.894.195-96")]
    [InlineData("023.583.060-05")]
    public void Check_MaskedValue_ReturnFalse(string cpfValue)
    {
        AssertInvalidCPF(cpfValue);
    }

    [Theory]
    [InlineData("0")]
    public void Check_ShortCPFValue_ThrowsCPFTooShortException(string cpfValue)
    {
        // Arrange
        var cpf = new Cpf(cpfValue);

        // Assert & Act
        Assert.Throws<CpfTooShortException>(() =>
        {
            cpf.Check();
        });
    }

    [Theory]
    [InlineData("123456789123456789")]
    public void Check_LongCPFValue_ThrowsCPFTooLongException(string cpfValue)
    {
        // Arrange
        var cpf = new Cpf(cpfValue);

        // Assert & Act
        Assert.Throws<CpfTooLongException>(() =>
        {
            cpf.Check();
        });
    }

    [Fact]
    public void Generate_CPFInstance_ReturnsMaskedCPF()
    {
        // Arrange
        var cpf = new Cpf();

        // Act
        var result = cpf.Generate(true, 1);

        // Assert
        Assert.Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", result.First());
    }

    [Fact]
    public void Generate_CPFInstance_ReturnsNonMaskedCPF()
    {
        // Arrange
        var cpf = new Cpf();

        // Act
        var result = cpf.Generate(false, 1);

        // Assert
        Assert.Matches(@"^\d{11}$", result.First());
    }

    private static void AssertValidCPF(string cpfValue)
    {
        // Arrange
        var cpf = new Cpf(cpfValue);

        // Act
        cpf.Check();

        // Asert
        Assert.True(cpf.IsValid);
    }

    private static void AssertInvalidCPF(string cpfValue)
    {
        // Arrange
        var cpf = new Cpf(cpfValue);

        // Act
        cpf.Check();

        // Asert
        Assert.False(cpf.IsValid);
    }
}
