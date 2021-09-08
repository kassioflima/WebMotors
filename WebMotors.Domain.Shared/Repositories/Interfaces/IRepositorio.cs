using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMotors.Domain.Shared.Repositories.Interfaces
{
    public interface IRepositorio<TEntity> : IDisposable where TEntity : class
    {
        Task<EntityEntry> InserirAsync(TEntity t);

        Task<IEnumerable<TEntity>> ConsultarAsync();

        TEntity Consultar(int id);

        Task<TEntity> ConsultarAsync(int id);

        Task<TEntity> AtualizarAsync(TEntity t, object key);

        Task<TEntity> ExcluirAsync(TEntity item);
    }
}
