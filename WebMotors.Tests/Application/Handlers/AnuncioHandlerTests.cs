using Microsoft.Extensions.Logging;
using Moq;
using WebMotors.Application.Application.Anuncios;
using WebMotors.Domain.Anuncios.Commands;
using WebMotors.Domain.Anuncios.Entities;
using WebMotors.Domain.Anuncios.Repositories.Interfaces;
using WebMotors.Domain.Shared.Commands.Interfaces;
using WebMotors.Domain.Shared.DomainNotifications;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;
using WebMotors.Domain.Shared.UoW.Interfaces;
using Xunit;

namespace WebMotors.Tests.Application.Handlers;

public class AnuncioInsertHandlerTests
{
    [Fact]
    public void AnuncioInsertHandler_Constructor_ShouldCreateSuccessfully()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioInsertHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        // Act
        var handler = new AnuncioInsertHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        // Assert
        Assert.NotNull(handler);
    }

    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldReturnResult()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioInsertHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioInsertHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

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
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Contains("inserido com sucesso", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithInvalidCommand_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioInsertHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioInsertHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        var command = new AnuncioInsertCommand
        {
            Marca = "", // Invalid
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("Parâmetros inválidos", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithNullCommand_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioInsertHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioInsertHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        // Act
        var result = await handler.HandleAsync(null);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("Parâmetros inválidos", result.Message);
    }
}

public class AnuncioUpdateHandlerTests
{
    [Fact]
    public void AnuncioUpdateHandler_Constructor_ShouldCreateSuccessfully()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioUpdateHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        // Act
        var handler = new AnuncioUpdateHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        // Assert
        Assert.NotNull(handler);
    }

    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldReturnResult()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioUpdateHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioUpdateHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

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

        var existingAnuncio = new Anuncio("Toyota", "Corolla", "XEI", 2023, 0, "Novo");
        
        repositoryMock.Setup(x => x.ConsultarAsync(1)).ReturnsAsync(existingAnuncio);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Contains("inserido com sucesso", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithNonExistentAnuncio_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioUpdateHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioUpdateHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        var command = new AnunciUpdateCommand
        {
            Id = 999,
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        repositoryMock.Setup(x => x.ConsultarAsync(999)).ReturnsAsync((Anuncio?)null);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("não encontrado", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithInvalidCommand_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioUpdateHandler>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioUpdateHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        var command = new AnunciUpdateCommand
        {
            Id = 0, // Invalid
            Marca = "Toyota",
            Modelo = "Corolla",
            Versao = "XEI",
            Ano = 2023,
            Quilometragem = 0,
            Observacao = "Novo"
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("Parâmetros inválidos", result.Message);
    }
}

public class AnuncioDeleteHandlerTests
{
    [Fact]
    public void AnuncioDeleteHandler_Constructor_ShouldCreateSuccessfully()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioDeleteCommand>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        // Act
        var handler = new AnuncioDeleteHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        // Assert
        Assert.NotNull(handler);
    }

    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldReturnResult()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioDeleteCommand>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioDeleteHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        var command = new AnuncioDeleteCommand
        {
            Id = 1
        };

        var existingAnuncio = new Anuncio("Toyota", "Corolla", "XEI", 2023, 0, "Novo");
        
        repositoryMock.Setup(x => x.Consultar(1)).Returns(existingAnuncio);
        repositoryMock.Setup(x => x.ExcluirAsync(existingAnuncio)).ReturnsAsync(existingAnuncio);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Contains("excluido com sucesso", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithNonExistentAnuncio_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioDeleteCommand>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioDeleteHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        var command = new AnuncioDeleteCommand
        {
            Id = 999
        };

        repositoryMock.Setup(x => x.Consultar(999)).Returns((Anuncio?)null!);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("não encontrado", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithInvalidCommand_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioDeleteCommand>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioDeleteHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        var command = new AnuncioDeleteCommand
        {
            Id = 0 // Invalid
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("Parâmetros inválidos", result.Message);
    }

    [Fact]
    public async Task HandleAsync_WithNullCommand_ShouldReturnFailure()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificationsHandlerMock = new Mock<IHandler<DomainNotification>>();
        var loggerMock = new Mock<ILogger<AnuncioDeleteCommand>>();
        var repositoryMock = new Mock<IAnuncioEFRepositorio>();

        var handler = new AnuncioDeleteHandler(
            unitOfWorkMock.Object,
            notificationsHandlerMock.Object,
            loggerMock.Object,
            repositoryMock.Object);

        // Act
        var result = await handler.HandleAsync(null);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("Parâmetros inválidos", result.Message);
    }
}