using System;
using System.Linq;
using WebMotors.Data.Context;
using WebMotors.Domain.Shared.Commands;
using WebMotors.Domain.Shared.UoW.Interfaces;

namespace WebMotors.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public CommandResponse Commit()
        {
            var rowsAffected = int.MinValue;

            try
            {
                rowsAffected = _context.SaveChanges();
                Console.WriteLine($"Commit: {rowsAffected} rows affected...");
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
                Console.WriteLine("Erro no commit: " + ex);
            }

            return new CommandResponse(rowsAffected > 0);
        }
        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
