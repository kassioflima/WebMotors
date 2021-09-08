using Flunt.Notifications;

namespace WebMotors.Domain.Shared.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        protected Entity()
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }

        public int Id { get; protected set; }
    }
}
