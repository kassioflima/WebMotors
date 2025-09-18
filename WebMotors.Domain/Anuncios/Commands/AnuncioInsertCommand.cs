using Flunt.Notifications;
using Flunt.Validations;
using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.Domain.Anuncios.Commands;

public class AnuncioInsertCommand : Notifiable<Notification>, ICommand
{
    public string Marca { get; set; }

    public string Modelo { get; set; }

    public string Versao { get; set; }

    public int Ano { get; set; }

    public int Quilometragem { get; set; }

    public string Observacao { get; set; }

    public bool EhValido()
    {
        AddNotifications(new Contract<AnuncioInsertCommand>()
            .Requires()
            .IsNotNullOrEmpty(Marca, "Marca", "Marca é obrigatório.")
            .IsGreaterThan(Marca, 1, "Marca", "O campo marca deve ter ao menos 1 caracteres.")
            .IsLowerOrEqualsThan(Marca, 45, "Marca", "O campo marca deve ser menor que 45 caracteres.")

            .IsNotNullOrEmpty(Modelo, "Modelo", "Modelo é obrigatório.")
            .IsGreaterThan(Modelo, 1, "Modelo", "O campo modelo deve ter ao menos 1 caracteres.")
            .IsLowerOrEqualsThan(Modelo, 45, "Modelo", "O campo modelo deve ser menor que 45 caracteres.")

            .IsNotNullOrEmpty(Versao, "Versao", "Versao é obrigatório.")
            .IsGreaterThan(Versao, 1, "Versao", "O campo versão deve ter ao menos 1 caracteres.")
            .IsLowerOrEqualsThan(Versao, 45, "Versao", "O campo versão deve ser menor que 45 caracteres.")

            .IsGreaterThan(Ano, 0, "Ano", "Preencha o campo ano.")

            .IsGreaterOrEqualsThan(Quilometragem, 0, "Quilometragem", "Preencha o campo quilometragem.")

            .IsNotNullOrEmpty(Observacao, "Observacao", "Observação é obrigatório.")
            .IsGreaterThan(Observacao, 1, "Observacao", "O campo observação deve ter ao menos 1 caracteres."));

        return IsValid;
    }
}
