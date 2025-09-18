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

public class AnuncioDeleteHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications, ILogger<AnuncioDeleteCommand> logger, IAnuncioEFRepositorio anuncioEFRepositorio = null) : CommandHandler(uow, notifications, logger), ICommandHandler<AnuncioDeleteCommand>, IAnuncioDeleteHandler
{
    private readonly IAnuncioEFRepositorio _anuncioEFRepositorio = anuncioEFRepositorio;
    private readonly ILogger<AnuncioDeleteCommand> _logger = logger;

    public async Task<ICommandResult> HandleAsync(AnuncioDeleteCommand command)
    {
        if (command != null && command.EhValido())
        {
            var result = _anuncioEFRepositorio.Consultar(command.Id);
            if (result == null)
                return new CommandResult(false, "Anuncio não encontrado para exclusão.", null);

            if (await IsDeleteDone(result))
                return new CommandResult(true, "Anuncio excluido com sucesso.", null);

            return new CommandResult(false, "Erro ao excluir um anuncio.", command?.Notifications);
        }

        return new CommandResult(false, "Parâmetros inválidos para excluir um anuncio.", command?.Notifications);
    }

    private async Task<bool> IsDeleteDone(Anuncio anuncio)
    {
        if (anuncio.IsValid)
        {
            var result = await DeleteAnuncio(anuncio);
            return result.Success && Commit();
        }
        return false;
    }

    private async Task<ICommandResult> DeleteAnuncio(Anuncio anuncio)
    {
        try
        {
            var entity = await _anuncioEFRepositorio.ExcluirAsync(anuncio);
            return new CommandResult(true, "Anuncio inserido com sucesso.", null);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro ao excluir anúncio. AnuncioId: {AnuncioId}, Marca: {Marca}, Modelo: {Modelo}",
                anuncio?.Id, anuncio?.Marca, anuncio?.Modelo);
            return new CommandResult(false, "Ocorreu uma falha ao excluir um anuncio.", (Status: "F", Mensagem: $"Falha: {ex}"));
        }
    }
}
