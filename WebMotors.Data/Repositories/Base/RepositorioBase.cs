using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Data.Context;
using WebMotors.Domain.Shared.Entities;
using WebMotors.Domain.Shared.Repositories.Interfaces;

namespace WebMotors.Data.Repositories.Base
{
    public class RepositorioBase<TEntity> : IRepositorio<TEntity> where TEntity : Entity
    {
        protected readonly DataContext Db;
        protected readonly DbSet<TEntity> DbSet;
        private bool disposed = false;

        public RepositorioBase(DataContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task Salvar(TEntity t)
        {
            Db.Entry(t).State = EntityState.Modified;
            await Db.SaveChangesAsync();
        }

        public virtual async Task<EntityEntry> InserirAsync(TEntity t)
        {
            return await DbSet.AddAsync(t);
        }

        public virtual async Task<TEntity> AtualizarAsync(TEntity t, object key)
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

        public async Task<IEnumerable<TEntity>> ConsultarAsync()
        {
            return await Db.Set<TEntity>().AsNoTracking()?.ToListAsync();
        }

        public async Task<TEntity> ConsultarAsync(int id)
        {
            return await Db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> ExcluirAsync(TEntity item)
        {
            try
            {
                TEntity exist = await DbSet.FindAsync(item.Id);
                if (exist != null)
                {
                    Db.Set<TEntity>().Remove(item);
                }
                return exist;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
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

        public TEntity Consultar(int id)
        {
            return Db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id).Result;
        }
    }
}
