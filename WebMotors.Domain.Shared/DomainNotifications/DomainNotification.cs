using System;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;

namespace WebMotors.Domain.Shared.DomainNotifications;

public class DomainNotification(string key, string value) : IDomainEvent
{
    public string Key { get; private set; } = key;
    public string Value { get; private set; } = value;
    public DateTime OccurrenceDate { get; private set; } = DateTime.Now;
    public int Version { get; private set; } = 1;
}
