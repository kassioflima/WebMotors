using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Domain.Shared.Models;
using WebMotors.Domain.Anuncios.Commands;
using WebMotors.Domain.Anuncios.Entities;
using WebMotors.Domain.Anuncios.Handlers.Interfaces;
using WebMotors.Domain.Anuncios.Repositories.Interfaces;
using WebMotors.Domain.Shared.Commands.Interfaces;

namespace WebMotors.API.Controllers.V1;

/// <summary>
/// Anuncio
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="anuncioInsertHandler"></param>
/// <param name="anuncioUpdateHandler"></param>
/// <param name="anuncioDeleteHandler"></param>
/// <param name="anuncioEFRepositorio"></param>
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
public class AnuncioController(IAnuncioInsertHandler anuncioInsertHandler, IAnuncioUpdateHandler anuncioUpdateHandler, IAnuncioDeleteHandler anuncioDeleteHandler, IAnuncioEFRepositorio anuncioEFRepositorio) : Controller
{
    private readonly IAnuncioInsertHandler _anuncioInsertHandler = anuncioInsertHandler;
    private readonly IAnuncioUpdateHandler _anuncioUpdateHandler = anuncioUpdateHandler;
    private readonly IAnuncioDeleteHandler _anuncioDeleteHandler = anuncioDeleteHandler;
    private readonly IAnuncioEFRepositorio _anuncioEFRepositorio = anuncioEFRepositorio;

    /// <summary>
    /// Obter Anuncios
    /// </summary>
    /// <returns></returns>
    [ActionName(nameof(ObterAnuncio))]
    [HttpGet]
    [Authorize(Policy = Policies.UserOrAdmin)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Anuncio>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<IActionResult> ObterAnuncio()
    {
        var result = await _anuncioEFRepositorio.ConsultarAsync();
        return Ok(result);
    }

    /// <summary>
    /// Obter Anuncio por ID
    /// </summary>
    /// <returns></returns>
    [ActionName(nameof(ObterAnuncioPorId))]
    [HttpGet("{id}")]
    [Authorize(Policy = Policies.UserOrAdmin)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Anuncio))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterAnuncioPorId(int id)
    {
        var result = await _anuncioEFRepositorio.ConsultarAsync(id);
        if (result != null)
            return Ok(result);

        return NotFound();
    }

    /// <summary>
    /// Inserir Anuncio
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [ActionName(nameof(CreateAnuncio))]
    [HttpPost]
    [Authorize(Policy = Policies.AdminOnly)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ICommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> CreateAnuncio(AnuncioInsertCommand command)
    {
        var result = await _anuncioInsertHandler.HandleAsync(command);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Atualizar Anuncio
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [ActionName(nameof(UpdateAnuncio))]
    [HttpPut]
    [Authorize(Policy = Policies.AdminOnly)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAnuncio(AnunciUpdateCommand command)
    {
        var result = await _anuncioUpdateHandler.HandleAsync(command);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Excluir Anuncio
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [ActionName(nameof(DeleteAnuncio))]
    [HttpDelete]
    [Authorize(Policy = Policies.AdminOnly)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAnuncio(AnuncioDeleteCommand command)
    {
        var result = await _anuncioDeleteHandler.HandleAsync(command);
        if (!result.Success)
            return BadRequest(result);

        return NoContent();
    }
}
