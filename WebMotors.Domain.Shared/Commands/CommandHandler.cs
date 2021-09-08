using Flunt.Notifications;
using Microsoft.Extensions.Logging;
using System;
using WebMotors.Domain.Shared.DomainNotifications;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;
using WebMotors.Domain.Shared.UoW.Interfaces;

namespace WebMotors.Domain.Shared.Commands
{
    public class CommandHandler : Notifiable<Notification>
    {
        private readonly IUnitOfWork _uow;
        private readonly IHandler<DomainNotification> _notifications;
        private readonly ILogger _logger;

        public CommandHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications, ILogger logger)
        {
            _uow = uow;
            _notifications = notifications;
            _logger = logger;
        }

        public bool Commit()
        {
            try
            {
                if (_notifications.HasNotifications())
                    return false;

                var commandResponse = _uow.Commit();
                if (commandResponse.Success)
                {
                    _logger.LogInformation("Commit Success.");
                    return true;
                }

                _logger.LogError("Erro ao persistir dados...");
                AddNotification("Commit", "Erro ao persistir dados.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao persistir dados.");
                throw new Exception("Erro ao salvar os dados...");
            }

        }
    }
}
