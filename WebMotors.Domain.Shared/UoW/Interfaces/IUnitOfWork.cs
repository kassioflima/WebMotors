using System;
using WebMotors.Domain.Shared.Commands;

namespace WebMotors.Domain.Shared.UoW.Interfaces;

public interface IUnitOfWork : IDisposable
{
    CommandResponse Commit();
}
