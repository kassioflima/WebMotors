using System;

namespace WebMotors.Domain.Shared.DomainNotifications.Interfaces
{
    public interface IDomainEvent
    {
        int Version { get; }
        DateTime OccurrenceDate { get; }
    }
}
