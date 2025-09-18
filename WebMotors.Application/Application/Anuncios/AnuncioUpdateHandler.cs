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

public class AnuncioUpdateHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications, ILogger<AnuncioUpdateHandler> logger, IAnuncioEFRepositorio anuncioEFRepositorio) : CommandHandler(uow, notifications, logger), ICommandHandler<AnunciUpdateCommand>, IAnuncioUpdateHandler
{
    private readonly IAnuncioEFRepositorio _anuncioEFRepositorio = anuncioEFRepositorio;
    private readonly ILogger<AnuncioUpdateHandler> _logger = logger;

    public async Task<ICommandResult> HandleAsync(AnunciUpdateCommand command)
    {
        if (command != null && command.EhValido())
        {
            var result = await _anuncioEFRepositorio.ConsultarAsync(command.Id);
            if (result == null)
                return new CommandResult(false, "Anuncio não encontrado.", null);

            result.Update(command.Marca, command.Modelo, command.Versao, command.Ano, command.Quilometragem, command.Observacao);

            if (await IsUpdateDone(result))
                return new CommandResult(true, "Anuncio inserido com sucesso.", result);

            return new CommandResult(false, "Erro ao inserir um anuncio.", result?.Notifications);
        }

        return new CommandResult(false, "Parâmetros inválidos para criar um anuncio.", command?.Notifications);
    }

    private async Task<bool> IsUpdateDone(Anuncio anuncio)
    {
        if (anuncio.IsValid)
        {
            var result = await UpdateAnuncio(anuncio);
            return result.Success && Commit();
        }
        return false;
    }

    private async Task<ICommandResult> UpdateAnuncio(Anuncio anuncio)
    {
        try
        {
            var entity = await _anuncioEFRepositorio.AtualizarAsync(anuncio, anuncio.Id);
            return new CommandResult(true, "Anuncio alterado com sucesso.", entity);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro ao atualizar anúncio. AnuncioId: {AnuncioId}, Marca: {Marca}, Modelo: {Modelo}",
                anuncio?.Id, anuncio?.Marca, anuncio?.Modelo);
            return new CommandResult(false, "Ocorreu uma falha ao alterar um anuncio.", (Status: "F", Mensagem: $"Falha: {ex}"));
        }
    }
}
