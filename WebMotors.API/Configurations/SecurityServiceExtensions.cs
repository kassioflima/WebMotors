using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using WebMotors.API.Security;
using WebMotors.Domain.Shared.Models;

namespace WebMotors.API.Configurations;

/// <summary>
/// Security Service Extensions
/// </summary>
public static class SecurityServiceExtensions
{
    /// <summary>
    /// Add Token Security
    /// </summary>
    /// <param name="services"></param>
    /// <param name="Configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddTokenSecurity(this IServiceCollection services, IConfiguration Configuration)
    {
        var signingConfigurations = new SigningConfigurations();
        services.AddSingleton(signingConfigurations);

        var tokenConfigurations = new TokenConfigurations();
        new ConfigureFromConfigurationOptions<TokenConfigurations>(
            Configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);
        services.AddSingleton(tokenConfigurations);


        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(bearerOptions =>
        {
            var paramsValidation = bearerOptions.TokenValidationParameters;
            paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            paramsValidation.ValidAudience = tokenConfigurations.Audience;
            paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            // Valida a assinatura de um token recebido
            paramsValidation.ValidateIssuerSigningKey = true;

            // Verifica se um token recebido ainda é válido
            paramsValidation.ValidateLifetime = true;

            // Tempo de tolerância para a expiração de um token (utilizado
            // caso haja problemas de sincronismo de horário entre diferentes
            // computadores envolvidos no processo de comunicação)
            paramsValidation.ClockSkew = TimeSpan.Zero;
        });

        // Configure authorization policies
        services.AddAuthorizationBuilder()
            .AddPolicy(Policies.AdminOnly, policy =>
            {
                policy.RequireRole(Roles.Admin);
            })
            .AddPolicy(Policies.UserOrAdmin, policy =>
            {
                policy.RequireRole(Roles.User, Roles.Admin);
            })
            .AddPolicy(Policies.ReadOnly, policy =>
            {
                policy.RequireRole(Roles.User, Roles.Admin);
                policy.RequireClaim("permission", "read");
            });

        return services;
    }
}
