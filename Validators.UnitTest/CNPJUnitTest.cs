namespace Validators.UnitTest;

public class CNPJUnitTest
{
    [Theory]
    [InlineData("12.ABC.345/01DE-35")]
    [InlineData("18.781.203/0001-28")]
    [InlineData("36.359.237/0001-00")]
    [InlineData("79.151.553/0001-03")]
    public void Validate_MaskedValue_ReturnTrue(string cnpjValue)
    {
        // Arrange
        var cnpj = new CNPJ(cnpjValue);

        // Act
        cnpj.Validate();

        // Asert
        Assert.True(cnpj.IsValid);
    }

    [Theory]
    [InlineData("12ABC34501DE35")]
    [InlineData("18781203000128")]
    public void Validate_NonMaskedValue_ReturnTrue(string cnpjValue)
    {
        // Arrange
        var cnpj = new CNPJ(cnpjValue);

        // Act
        cnpj.Validate();

        // Asert
        Assert.True(cnpj.IsValid);
    }

    [Theory]
    [InlineData("0")]

    public void Validate_ShortCNPJ_ThrowsCNPJIncorrectFormatException(string cnpjValue)
    {
        // Arrange
        var cnpj = new CNPJ(cnpjValue);

        // Assert & Act
        Assert.Throws<CNPJTooShortException>(() =>
        {
            cnpj.Validate();
        });
    }

    [Theory]
    [InlineData("123456789123456789")]
    public void Validate_LongCNPJ_ThrowsCNPJIncorrectFormatException(string cnpjValue)
    {
        // Arrange
        var cnpj = new CNPJ(cnpjValue);

        // Assert & Act
        Assert.Throws<CNPJTooLongException>(() =>
        {
            cnpj.Validate();
        });
    }
}
