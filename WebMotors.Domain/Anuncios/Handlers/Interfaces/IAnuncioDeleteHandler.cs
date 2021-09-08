using System.Threading.Tasks;
using WebMotors.Domain.Anuncios.Commands;
using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.Domain.Anuncios.Handlers.Interfaces
{
    public interface IAnuncioDeleteHandler
    {
        Task<ICommandResult> HandleAsync(AnuncioDeleteCommand command);
    }
}
