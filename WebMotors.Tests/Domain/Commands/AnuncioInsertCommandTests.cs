using WebMotors.Domain.Anuncios.Commands;
using Xunit;

namespace WebMotors.Tests.Domain.Commands;

public class AnuncioInsertCommandTests
{
    [Fact]
    public void AnuncioInsertCommand_WithValidData_ShouldBeValid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
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
    public void AnuncioInsertCommand_WithEmptyMarca_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
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

    [Fact]
    public void AnuncioInsertCommand_WithNullMarca_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = null,
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
    public void AnuncioInsertCommand_WithEmptyModelo_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = "Toyota",
            Modelo = "",
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
    public void AnuncioInsertCommand_WithEmptyVersao_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "",
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
    public void AnuncioInsertCommand_WithZeroAno_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 0,
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
    public void AnuncioInsertCommand_WithNegativeQuilometragem_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = -1,
            Observacao = "Novo"
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Fact]
    public void AnuncioInsertCommand_WithEmptyObservacao_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = ""
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
    }

    [Fact]
    public void AnuncioInsertCommand_WithLongMarca_ShouldBeInvalid()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = new string('A', 46), // 46 caracteres
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
    [InlineData("Toyota", "Corolla", "XEI", 2023, 0, "Novo")]
    [InlineData("Honda", "Civic", "LX", 2022, 15000, "Semi-novo")]
    [InlineData("Ford", "Focus", "SE", 2021, 30000, "Usado")]
    public void AnuncioInsertCommand_WithValidDataTheory_ShouldBeValid(
        string marca, string modelo, string versao, int ano, int quilometragem, string observacao)
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
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

    [Fact]
    public void AnuncioInsertCommand_WithMultipleInvalidFields_ShouldHaveMultipleNotifications()
    {
        // Arrange
        var command = new AnuncioInsertCommand
        {
            Marca = "",
            Modelo = "",
            Versao = "",
            Ano = 0,
            Quilometragem = -1,
            Observacao = ""
        };

        // Act
        var isValid = command.EhValido();

        // Assert
        Assert.False(isValid);
        Assert.NotEmpty(command.Notifications);
        Assert.True(command.Notifications.Count > 1);
    }
}