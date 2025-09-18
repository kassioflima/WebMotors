using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Application.Application.Anuncios;
using WebMotors.Data.Context;
using WebMotors.Data.Repositories.Anuncios;
using WebMotors.Data.UoW;
using WebMotors.Domain.Anuncios.Handlers.Interfaces;
using WebMotors.Domain.Anuncios.Repositories.Interfaces;
using WebMotors.Domain.Shared.Commands.Interfaces;
using WebMotors.Domain.Shared.Commands.Response;
using WebMotors.Domain.Shared.DomainNotifications;
using WebMotors.Domain.Shared.DomainNotifications.Interfaces;
using WebMotors.Domain.Shared.UoW.Interfaces;

namespace WebMotors.Infra.CrossCutting.IoC;

public abstract class NativeInjectorBootStrapper
{
    /// <summary>
    /// Injections dependence
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterServices(IServiceCollection services)
    {
        #region[-- Handlers --]
        services.AddScoped<IAnuncioInsertHandler, AnuncioInsertHandler>();
        services.AddScoped<IAnuncioUpdateHandler, AnuncioUpdateHandler>();
        services.AddScoped<IAnuncioDeleteHandler, AnuncioDeleteHandler>();
        #endregion

        #region[-- Repositorios EF Core --]
        services.AddScoped<DataContext, DataContext>();
        services.AddScoped<IAnuncioEFRepositorio, AnuncioEFRepositorio>();
        #endregion

        #region[-- ExternalData --]
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICommandResult>(provider =>
            new CommandResult(false, string.Empty));
        services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion
    }
}
