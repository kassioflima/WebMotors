using System;
using System.Collections.Generic;

namespace WebMotors.Domain.Shared.DomainNotifications.Interfaces
{
    public interface IHandler<T> : IDisposable where T : IDomainEvent
    {
        void Handle(T args);
        bool HasNotifications();
        List<T> GetValues();
    }
}
