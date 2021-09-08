using System.Threading.Tasks;
using WebMotors.Domain.Anuncios.Commands;
using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.Domain.Anuncios.Handlers.Interfaces
{
    public interface IAnuncioUpdateHandler
    {
        Task<ICommandResult> HandleAsync(AnunciUpdateCommand command);
    }
}
