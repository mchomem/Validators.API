namespace DocBrNet.UnitTest;

public class CnpjUnitTest
{
    [Theory]
    [InlineData("12.ABC.345/01DE-35")]
    [InlineData("18.781.203/0001-28")]
    [InlineData("36.359.237/0001-00")]
    [InlineData("79.151.553/0001-03")]
    [InlineData("65.247.902/8452-02")]
    public void Validate_MaskedValue_ReturnTrue(string cnpjValue)
    {
        AssertValidCNPJ(cnpjValue);
    }

    [Theory]
    [InlineData("12ABC34501DE35")]
    [InlineData("18781203000128")]
    [InlineData("36359237000100")]
    [InlineData("79151553000103")]
    [InlineData("65247902845202")]
    public void Validate_NonMaskedValue_ReturnTrue(string cnpjValue)
    {
        AssertValidCNPJ(cnpjValue);
    }

    [Theory]
    [InlineData("T0.CXZ.WS0/0001-35")]
    [InlineData("85.63F.EDC/0001-28")]
    [InlineData("SX.EBH.XKL/0001-00")]
    [InlineData("CS.0XQ.KL8/0001-03")]
    [InlineData("8J.LOV.XAP/0001-02")]
    public void Validate_MaskedValue_ReturnFalse(string cnpjValue)
    {
        AssertInvalidCNPJ(cnpjValue);
    }

    [Theory]
    [InlineData("0")]
    public void Validate_ShortCNPJ_ThrowsCNPJIncorrectFormatException(string cnpjValue)
    {
        // Arrange
        var cnpj = new Cnpj(cnpjValue);

        // Assert & Act
        Assert.Throws<CnpjTooShortException>(() =>
        {
            cnpj.Validate();
        });
    }

    [Theory]
    [InlineData("123456789123456789")]
    public void Validate_LongCNPJ_ThrowsCNPJIncorrectFormatException(string cnpjValue)
    {
        // Arrange
        var cnpj = new Cnpj(cnpjValue);

        // Assert & Act
        Assert.Throws<CnpjTooLongException>(() =>
        {
            cnpj.Validate();
        });
    }

    [Fact]
    public void Generate_CNPJInstance_ReturnsMaskedCNPJ()
    {
        // Arrange
        var cnpj = new Cnpj();

        // Act
        var result = cnpj.Generate(TypeCNPJ.Numeric, true, 1);

        // Assert
        Assert.Matches(@"\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}", result.First());
    }

    [Fact]
    public void Generate_CNPJInstance_ReturnsNonMaskedCNPJ()
    {
        // Arrange
        var cnpj = new Cnpj();

        // Act
        var result = cnpj.Generate(TypeCNPJ.Numeric, false, 1);

        // Assert
        Assert.Matches(@"\d{14}", result.First());
    }

    [Fact]
    public void Generate_CNPJInstance_ReturnCNPJMaximumQuantityAllowedException()
    {
        // Arrange
        var cnpj = new Cnpj();
        
        // Assert & Act
        Assert.Throws<CnpjMaximumQuantityAllowedException>(() =>
        {
            cnpj.Generate(TypeCNPJ.Numeric, false, 1001);
        });
    }

    private static void AssertValidCNPJ(string cnpjValue)
    {
        // Arrange
        var cnpj = new Cnpj(cnpjValue);

        // Act
        cnpj.Validate();

        // Asert
        Assert.True(cnpj.IsValid);
    }

    private static void AssertInvalidCNPJ(string cnpjValue)
    {
        // Arrange
        var cnpj = new Cnpj(cnpjValue);

        // Act
        cnpj.Validate();

        // Asert
        Assert.False(cnpj.IsValid);
    }
}
