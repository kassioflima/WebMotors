using Flunt.Notifications;
using Flunt.Validations;
using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.Domain.Anuncios.Commands
{
    public class AnuncioDeleteCommand : Notifiable<Notification>, ICommand
    {
        public int Id { get; set; }

        public bool EhValido()
        {
            AddNotifications(new Contract<AnuncioDeleteCommand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "O campo id deve ser maior que zero.")
                );

            return IsValid;
        }
    }
}
