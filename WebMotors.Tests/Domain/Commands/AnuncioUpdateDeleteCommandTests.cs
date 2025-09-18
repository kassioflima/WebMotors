using WebMotors.Domain.Anuncios.Commands;
using Xunit;

namespace WebMotors.Tests.Domain.Commands;

public class AnuncioUpdateCommandTests
{
    [Fact]
    public void AnuncioUpdateCommand_WithValidData_ShouldBeValid()
    {
        // Arrange
        var command = new AnunciUpdateCommand
        {
            Id = 1,
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.True(isValid);
        Assert.Empty(command.Notifications);
    }

    [Fact]
    public void AnuncioUpdateCommand_WithZeroId_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnunciUpdateCommand
        {
            Id = 0,
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Fact]
    public void AnuncioUpdateCommand_WithNegativeId_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnunciUpdateCommand
        {
            Id = -1,
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Fact]
    public void AnuncioUpdateCommand_WithEmptyMarca_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnunciUpdateCommand
        {
            Id = 1,
            Marca = "",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Theory]
    [InlineData(1, "Toyota", "Corolla", "XEI", 2023, 0, "Novo")]
    [InlineData(2, "Honda", "Civic", "LX", 2022, 15000, "Semi-novo")]
    [InlineData(3, "Ford", "Focus", "SE", 2021, 30000, "Usado")]
    public void AnuncioUpdateCommand_WithValidDataTheory_ShouldBeValid(
        int id, string marca, string modelo, string versao, int ano, int quilometragem, string observacao)
    {
        // Arrange
        var command = new AnunciUpdateCommand
        {
            Id = id,
            Marca = marca,
            Modelo = modelo,
            Versao = versao,
            Ano = ano,
            Quilometragem = quilometragem,
            Observacao = observacao
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.True(isValid);
        Assert.Empty(command.Notifications);
    }
}

public class AnuncioDeleteCommandTests
{
    [Fact]
    public void AnuncioDeleteCommand_WithValidId_ShouldBeValid()
    {
        // Arrange
        var command = new AnuncioDeleteCommand
        {
            Id = 1
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.True(isValid);
        Assert.Empty(command.Notifications);
    }

    [Fact]
    public void AnuncioDeleteCommand_WithZeroId_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioDeleteCommand
        {
            Id = 0
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Fact]
    public void AnuncioDeleteCommand_WithNegativeId_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioDeleteCommand
        {
            Id = -1
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    [InlineData(999)]
    public void AnuncioDeleteCommand_WithValidIds_ShouldBeValid(int id)
    {
        // Arrange
        var command = new AnuncioDeleteCommand
        {
            Id = id
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.True(isValid);
        Assert.Empty(command.Notifications);
    }
}