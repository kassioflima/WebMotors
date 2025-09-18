using Microsoft.AspNetCore.Identity;
using System;

namespace WebMotors.Domain.Shared.Models;

/// <summary>
/// Modelo de usuário para autenticação
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Nome completo do usuário
    /// </summary>
    public string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Data de criação do usuário
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Indica se o usuário está ativo
    /// </summary>
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Roles do sistema
/// </summary>
public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}

/// <summary>
/// Políticas de autorização
/// </summary>
public static class Policies
{
    public const string AdminOnly = "AdminOnly";
    public const string UserOrAdmin = "UserOrAdmin";
    public const string ReadOnly = "ReadOnly";
}
