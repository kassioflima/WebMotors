namespace WebMotors.Domain.Shared.Commands.Interfaces
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
