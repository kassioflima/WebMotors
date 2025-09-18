using WebMotors.Data.Context;
using WebMotors.Data.Repositories.Base;
using WebMotors.Domain.Anuncios.Entities;
using WebMotors.Domain.Anuncios.Repositories.Interfaces;

namespace WebMotors.Data.Repositories.Anuncios;

public class AnuncioEFRepositorio(DataContext db) : RepositorioBase<Anuncio>(db), IAnuncioEFRepositorio
{
}
