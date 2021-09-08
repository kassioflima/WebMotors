using WebMotors.Domain.Shared.Entities;

namespace WebMotors.Domain.Anuncios.Entities
{
    public class Anuncio : Entity
    {
        public string Marca { get; private set; }

        public string Modelo { get; private set; }

        public string Versao { get; private set; }

        public int Ano { get; private set; }

        public int Quilometragem { get; private set; }

        public string Observacao { get; private set; }

        protected Anuncio() { }

        public Anuncio(string marca, string modelo, string versao, int ano, int quilometragem, string observacao)
        {
            Marca = marca;
            Modelo = modelo;
            Versao = versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }

        public void Atualisar(int id)
        {
            Id = id;
        }

    }
}
