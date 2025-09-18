using Serilog;
using System;
using System.Linq;
using WebMotors.Data.Context;
using WebMotors.Domain.Shared.Commands;
using WebMotors.Domain.Shared.UoW.Interfaces;

namespace WebMotors.Data.UoW;

public class UnitOfWork(DataContext context) : IUnitOfWork
{
    private readonly DataContext _context = context;

    public CommandResponse Commit()
    {
        var rowsAffected = int.MinValue;

        try
        {
            rowsAffected = _context.SaveChanges();
            Log.Information("Commit realizado com sucesso. Rows affected: {RowsAffected}", rowsAffected);
        }
        catch (Exception ex)
        {
            rowsAffected = 0;
            Log.Error(ex, "Erro no commit do UnitOfWork. Context: {ContextType}", _context.GetType().Name);
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
