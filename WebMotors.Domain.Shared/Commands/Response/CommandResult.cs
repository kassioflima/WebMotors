using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.Domain.Shared.Commands.Response;

public class CommandResult(bool success, string message, object data = null) : ICommandResult
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
    public object Data { get; set; } = data;
}
