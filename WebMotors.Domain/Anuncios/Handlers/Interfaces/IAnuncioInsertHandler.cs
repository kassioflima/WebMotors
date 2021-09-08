using System.Threading.Tasks;
using WebMotors.Domain.Anuncios.Commands;
using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.Domain.Anuncios.Handlers.Interfaces
{
    public interface IAnuncioInsertHandler
    {
        Task<ICommandResult> HandleAsync(AnuncioInsertCommand command);
    }
}
