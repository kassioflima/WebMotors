using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;
using WebMotors.Domain.Anuncios.Commands;
using WebMotors.Domain.Anuncios.Entities;
using WebMotors.Domain.Anuncios.Handlers.Interfaces;
using WebMotors.Domain.Anuncios.Repositories.Interfaces;
using WebMotors.Domain.Shared.Commands;
using WebMotors.Domain.Shared.Commands.Interfaces;
using WebMotors.Domain.Shared.Commands.Response;
using WebMotors.Domain.Shared.DomainNotifications;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;
using WebMotors.Domain.Shared.UoW.Interfaces;

namespace WebMotors.Application.Application.Anuncios;

public class AnuncioInsertHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications, ILogger<AnuncioInsertHandler> logger, IAnuncioEFRepositorio anuncioEFRepositorio) : CommandHandler(uow, notifications, logger), ICommandHandler<AnuncioInsertCommand>, IAnuncioInsertHandler
{
    private readonly IAnuncioEFRepositorio _anuncioEFRepositorio = anuncioEFRepositorio;
    private readonly ILogger<AnuncioInsertHandler> _logger = logger;

    public async Task<ICommandResult> HandleAsync(AnuncioInsertCommand command)
    {
        if (command != null && command.EhValido())
        {
            var entity = ParseToEntity(command);
            if (await IsInsertDone(entity))
                return new CommandResult(true, "Anuncio inserido com sucesso.", entity);

            return new CommandResult(false, "Erro ao inserir um anuncio.", entity?.Notifications);
        }

        return new CommandResult(false, "Parâmetros inválidos para criar um anuncio.", command?.Notifications);
    }

    private static Anuncio ParseToEntity(AnuncioInsertCommand command)
    {
        return new Anuncio(command.Marca, command.Modelo, command.Versao, command.Ano, command.Quilometragem, command.Observacao);
    }

    private async Task<bool> IsInsertDone(Anuncio anuncio)
    {
        if (anuncio.IsValid)
        {
            var result = await InsertAnuncio(anuncio);
            return result.Success && Commit();
        }
        return false;
    }

    private async Task<ICommandResult> InsertAnuncio(Anuncio anuncio)
    {
        try
        {
            var entity = await _anuncioEFRepositorio.InserirAsync(anuncio);
            return new CommandResult(true, "Anuncio inserido com sucesso.", entity);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro ao inserir anúncio. AnuncioId: {AnuncioId}, Marca: {Marca}, Modelo: {Modelo}",
                anuncio?.Id, anuncio?.Marca, anuncio?.Modelo);
            return new CommandResult(false, "Ocorreu uma falha ao inserir um anuncio.", (Status: "F", Mensagem: $"Falha: {ex}"));
        }
    }
}
