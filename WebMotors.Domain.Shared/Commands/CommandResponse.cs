namespace WebMotors.Domain.Shared.Commands
{
    public class CommandResponse
    {
        public CommandResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
