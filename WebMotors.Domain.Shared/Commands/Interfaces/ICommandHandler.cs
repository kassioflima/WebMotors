using System.Threading.Tasks;

namespace WebMotors.Domain.Shared.Commands.Interfaces;

public interface ICommandHandler<Command> where Command : ICommand
{
    Task<ICommandResult> HandleAsync(Command command);
}
