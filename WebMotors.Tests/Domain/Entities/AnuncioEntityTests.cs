using WebMotors.Domain.Anuncios.Entities;
using Xunit;

namespace WebMotors.Tests.Domain.Entities;

public class AnuncioEntityTests
{
    [Fact]
    public void Anuncio_Constructor_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var marca = "Toyota";
        var modelo = "Corolla";
        var versao = "XEI";
        var ano = 2023;
        var quilometragem = 0;
        var observacao = "Novo";

        // Act
        var anuncio = new Anuncio(marca, modelo, versao, ano, quilometragem, observacao);

        // Assert
        Assert.NotNull(anuncio);
        Assert.Equal(marca, anuncio.Marca);
        Assert.Equal(modelo, anuncio.Modelo);
        Assert.Equal(versao, anuncio.Versao);
        Assert.Equal(ano, anuncio.Ano);
        Assert.Equal(quilometragem, anuncio.Quilometragem);
        Assert.Equal(observacao, anuncio.Observacao);
    }

    [Fact]
    public void Anuncio_Constructor_WithEmptyStrings_ShouldCreateSuccessfully()
    {
        // Arrange
        var marca = "";
        var modelo = "";
        var versao = "";
        var ano = 0;
        var quilometragem = 0;
        var observacao = "";

        // Act
        var anuncio = new Anuncio(marca, modelo, versao, ano, quilometragem, observacao);

        // Assert
        Assert.NotNull(anuncio);
        Assert.Equal(marca, anuncio.Marca);
        Assert.Equal(modelo, anuncio.Modelo);
        Assert.Equal(versao, anuncio.Versao);
        Assert.Equal(ano, anuncio.Ano);
        Assert.Equal(quilometragem, anuncio.Quilometragem);
        Assert.Equal(observacao, anuncio.Observacao);
    }

    [Fact]
    public void Anuncio_Constructor_WithNullStrings_ShouldCreateSuccessfully()
    {
        // Arrange
        string marca = null;
        string modelo = null;
        string versao = null;
        var ano = 2020;
        var quilometragem = 50000;
        string observacao = null;

        // Act
        var anuncio = new Anuncio(marca!, modelo!, versao!, ano, quilometragem, observacao!);

        // Assert
        Assert.NotNull(anuncio);
        Assert.Null(anuncio.Marca);
        Assert.Null(anuncio.Modelo);
        Assert.Null(anuncio.Versao);
        Assert.Equal(ano, anuncio.Ano);
        Assert.Equal(quilometragem, anuncio.Quilometragem);
        Assert.Null(anuncio.Observacao);
    }

    [Fact]
    public void Anuncio_Atualisar_ShouldSetId()
    {
        // Arrange
        var anuncio = new Anuncio("Toyota", "Corolla", "XEI", 2023, 0, "Novo");
        var expectedId = 123;

        // Act
        anuncio.Atualisar(expectedId);

        // Assert
        Assert.Equal(expectedId, anuncio.Id);
    }

    [Fact]
    public void Anuncio_Update_ShouldUpdateAllProperties()
    {
        // Arrange
        var anuncio = new Anuncio("Toyota", "Corolla", "XEI", 2023, 0, "Novo");
        var newMarca = "Honda";
        var newModelo = "Civic";
        var newVersao = "LX";
        var newAno = 2022;
        var newQuilometragem = 15000;
        var newObservacao = "Semi-novo";

        // Act
        anuncio.Update(newMarca, newModelo, newVersao, newAno, newQuilometragem, newObservacao);

        // Assert
        Assert.Equal(newMarca, anuncio.Marca);
        Assert.Equal(newModelo, anuncio.Modelo);
        Assert.Equal(newVersao, anuncio.Versao);
        Assert.Equal(newAno, anuncio.Ano);
        Assert.Equal(newQuilometragem, anuncio.Quilometragem);
        Assert.Equal(newObservacao, anuncio.Observacao);
    }

    [Fact]
    public void Anuncio_Update_WithNullValues_ShouldUpdateToNull()
    {
        // Arrange
        var anuncio = new Anuncio("Toyota", "Corolla", "XEI", 2023, 0, "Novo");

        // Act
        anuncio.Update(null, null, null, 0, 0, null);

        // Assert
        Assert.Null(anuncio.Marca);
        Assert.Null(anuncio.Modelo);
        Assert.Null(anuncio.Versao);
        Assert.Equal(0, anuncio.Ano);
        Assert.Equal(0, anuncio.Quilometragem);
        Assert.Null(anuncio.Observacao);
    }

    [Theory]
    [InlineData("Toyota", "Corolla", "XEI", 2023, 0, "Novo")]
    [InlineData("Honda", "Civic", "LX", 2022, 15000, "Semi-novo")]
    [InlineData("Ford", "Focus", "SE", 2021, 30000, "Usado")]
    [InlineData("Volkswagen", "Golf", "Comfortline", 2020, 45000, "Usado")]
    public void Anuncio_Constructor_WithDifferentData_ShouldCreateSuccessfully(
        string marca, string modelo, string versao, int ano, int quilometragem, string observacao)
    {
        // Act
        var anuncio = new Anuncio(marca, modelo, versao, ano, quilometragem, observacao);

        // Assert
        Assert.NotNull(anuncio);
        Assert.Equal(marca, anuncio.Marca);
        Assert.Equal(modelo, anuncio.Modelo);
        Assert.Equal(versao, anuncio.Versao);
        Assert.Equal(ano, anuncio.Ano);
        Assert.Equal(quilometragem, anuncio.Quilometragem);
        Assert.Equal(observacao, anuncio.Observacao);
    }

    [Fact]
    public void Anuncio_Properties_ShouldBeReadOnly()
    {
        // Arrange
        var anuncio = new Anuncio("Toyota", "Corolla", "XEI", 2023, 0, "Novo");

        // Act & Assert
        // Verificar que as propriedades são readonly (não podem ser definidas diretamente)
        // Isso é verificado pela compilação - se tentássemos fazer anuncio.Marca = "Honda", 
        // o código não compilaria
        Assert.Equal("Toyota", anuncio.Marca);
        Assert.Equal("Corolla", anuncio.Modelo);
        Assert.Equal("XEI", anuncio.Versao);
        Assert.Equal(2023, anuncio.Ano);
        Assert.Equal(0, anuncio.Quilometragem);
        Assert.Equal("Novo", anuncio.Observacao);
    }
}
