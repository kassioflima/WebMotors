using System;
using System.Collections.Generic;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;

namespace WebMotors.Domain.Shared.DomainNotifications
{
    public class DomainNotificationHandler : IHandler<DomainNotification>
    {
        private List<DomainNotification> notifications;

        public DomainNotificationHandler()
        {
            notifications = new List<DomainNotification>();
        }

        public void Handle(DomainNotification args)
        {
            notifications.Add(args);
        }

        public List<DomainNotification> GetValues()
        {
            return notifications;
        }

        public bool HasNotifications()
        {
            return GetValues().Count > 0;
        }

        public void Dispose()
        {
            this.notifications = new List<DomainNotification>();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;
            }
        }
    }
}
