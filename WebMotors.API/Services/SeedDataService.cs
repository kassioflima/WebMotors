using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Domain.Shared.Models;

namespace WebMotors.API.Services;

/// <summary>
/// Serviço responsável por criar usuários e roles iniciais
/// </summary>
public class SeedDataService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SeedDataService> _logger;

    public SeedDataService(IServiceProvider serviceProvider, ILogger<SeedDataService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Executa o seeding dos dados iniciais
    /// </summary>
    public async Task SeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        try
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao executar o seeding dos dados iniciais");
            throw;
        }
    }

    /// <summary>
    /// Cria as roles iniciais do sistema
    /// </summary>
    private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { Roles.Admin, Roles.User };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                _logger.LogInformation("Role '{Role}' criada com sucesso", role);
            }
            else
            {
                _logger.LogInformation("Role '{Role}' já existe", role);
            }
        }
    }

    /// <summary>
    /// Cria os usuários iniciais do sistema
    /// </summary>
    private async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
    {
        // Criar usuário administrador
        await CreateAdminUserAsync(userManager);

        // Criar usuário comum com perfil de leitura
        await CreateReadOnlyUserAsync(userManager);
    }

    /// <summary>
    /// Cria o usuário administrador
    /// </summary>
    private async Task CreateAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@webmotors.com";
        const string adminPassword = "Admin@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Administrador do Sistema",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                _logger.LogInformation("Usuário administrador criado com sucesso: {Email}", adminEmail);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Erro ao criar usuário administrador: {Errors}", errors);
            }
        }
        else
        {
            _logger.LogInformation("Usuário administrador já existe: {Email}", adminEmail);
        }
    }

    /// <summary>
    /// Cria o usuário comum com perfil de somente leitura
    /// </summary>
    private async Task CreateReadOnlyUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string readOnlyEmail = "leitura@webmotors.com";
        const string readOnlyPassword = "Leitura@123";

        var readOnlyUser = await userManager.FindByEmailAsync(readOnlyEmail);
        if (readOnlyUser == null)
        {
            readOnlyUser = new ApplicationUser
            {
                UserName = readOnlyEmail,
                Email = readOnlyEmail,
                EmailConfirmed = true,
                FullName = "Usuário de Leitura",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(readOnlyUser, readOnlyPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(readOnlyUser, Roles.User);
                _logger.LogInformation("Usuário de leitura criado com sucesso: {Email}", readOnlyEmail);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Erro ao criar usuário de leitura: {Errors}", errors);
            }
        }
        else
        {
            _logger.LogInformation("Usuário de leitura já existe: {Email}", readOnlyEmail);
        }
    }
}
