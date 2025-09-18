namespace WebMotors.Domain.Shared.Commands;

public class CommandResponse(bool success = false)
{
    public bool Success { get; private set; } = success;
}
