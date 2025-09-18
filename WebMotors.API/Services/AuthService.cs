using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMotors.API.Models;
using WebMotors.API.Security;
using WebMotors.Domain.Shared.Models;

namespace WebMotors.API.Services;

/// <summary>
/// Serviço de autenticação
/// </summary>
public class AuthService
{
    private readonly UserManager<WebMotors.Domain.Shared.Models.ApplicationUser> _userManager;
    private readonly SignInManager<WebMotors.Domain.Shared.Models.ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly TokenConfigurations _tokenConfigurations;

    public AuthService(
        UserManager<WebMotors.Domain.Shared.Models.ApplicationUser> userManager,
        SignInManager<WebMotors.Domain.Shared.Models.ApplicationUser> signInManager,
        IConfiguration configuration,
        ILogger<AuthService> logger,
        SigningConfigurations signingConfigurations,
        TokenConfigurations tokenConfigurations)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _logger = logger;
        _signingConfigurations = signingConfigurations;
        _tokenConfigurations = tokenConfigurations;
    }

    /// <summary>
    /// Realiza login do usuário
    /// </summary>
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Credenciais inválidas"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Credenciais inválidas"
                };
            }

            var token = await GenerateJwtTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            _logger.LogInformation("Usuário {Email} fez login com sucesso", user.Email);

            return new AuthResponse
            {
                Success = true,
                Message = "Login realizado com sucesso",
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                User = new UserInfo
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Roles = roles.ToList()
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar login para {Email}", request.Email);
            return new AuthResponse
            {
                Success = false,
                Message = "Erro interno do servidor"
            };
        }
    }

    /// <summary>
    /// Registra novo usuário
    /// </summary>
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Email já está em uso"
                };
            }

            var user = new WebMotors.Domain.Shared.Models.ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Erro ao criar usuário: {errors}"
                };
            }

            // Adiciona role de usuário padrão
            await _userManager.AddToRoleAsync(user, WebMotors.Domain.Shared.Models.Roles.User);

            _logger.LogInformation("Novo usuário registrado: {Email}", user.Email);

            return new AuthResponse
            {
                Success = true,
                Message = "Usuário registrado com sucesso"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar usuário {Email}", request.Email);
            return new AuthResponse
            {
                Success = false,
                Message = "Erro interno do servidor"
            };
        }
    }

    /// <summary>
    /// Gera token JWT
    /// </summary>
    private async Task<string> GenerateJwtTokenAsync(WebMotors.Domain.Shared.Models.ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        claims.AddRange(roleClaims);

        var token = new JwtSecurityToken(
            issuer: _tokenConfigurations.Issuer,
            audience: _tokenConfigurations.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: _signingConfigurations.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
