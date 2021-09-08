using Dotz.Domain.Shared.Commands;
using Dotz.Domain.Shared.UoW.Interfaces;
using Dotz.Infra.Data.MySql.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dotz.Infra.Data.MySql.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DotzDbContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(DotzDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public CommandResponse Commit()
        {
            int rowsAffected;
            try
            {
                rowsAffected = _context.SaveChanges();
                _logger.LogInformation($"Commit: {rowsAffected} rows affected...");
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
                _logger.LogError(ex, "Erro no commit: ");
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
                _logger.LogInformation("Dispose context...");
                _context.Dispose();
            }
        }
    }
}