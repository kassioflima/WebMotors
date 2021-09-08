using Dotz.Domain.Shared.Entities;
using Dotz.Domain.Shared.Repositories.Interfaces;
using Dotz.Infra.Data.MySql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dotz.Infra.Data.MySql.Repositories.Base
{
    public class RepositorioBase<TEntity> : IRepositorio<TEntity> where TEntity : Entity
    {
        protected readonly DotzDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        private bool disposed = false;

        public RepositorioBase(DotzDbContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<EntityEntry> AddEntity(TEntity t)
        {
            return await DbSet.AddAsync(t);
        }

        public virtual async Task<TEntity> Atualizar(TEntity t, object key)
        {
            if (t == null)
                return null;
            TEntity exist = await DbSet.FindAsync(key);
            if (exist != null)
            {
                Db.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        public async Task<TEntity> Consultar(Guid id)
        {
            return await Db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> Excluir(TEntity item)
        {
            TEntity exist = await DbSet.FindAsync(item);
            if (exist != null)
            {
                Db.Set<TEntity>().Remove(item);
            }
            return exist;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
