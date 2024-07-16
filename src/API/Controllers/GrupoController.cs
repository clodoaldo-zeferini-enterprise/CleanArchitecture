using Application.CQRS.Command.Grupo.Commands;
using Application.CQRS.Query.NoSQL.Grupo.Queries;
using Application.CQRS.Query.SQL.Grupo.Queries;
using Application.DTO.Request;
using Application.DTO.Response.Grupo;
using Infrastructure.Base.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class GrupoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    public GrupoController(IConfiguration configuration, IMediator mediator)
    {
        _mediator = mediator;
       // _logger = logger;
       _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetGrupos([FromQuery] GetRequest getRequest)
    {
        try
        {
            //_logger.LogInformation("Estou no Index do HomeController");

            var dbServer = _configuration.GetValue<EDataBaseName>("DBServer");
            Object query;
            Object members;

            switch (dbServer)
            {
                case EDataBaseName.DynamoDB:                    
                    query = new GetGrupoFromNoSQL(getRequest.PageNumber, getRequest.PageSize, getRequest.FiltraNome, getRequest.FiltroNome, getRequest.FiltraInsertedOn, getRequest.DataInicial, getRequest.DataFinal, getRequest.FiltraStatus, getRequest.Status, getRequest.FiltraPorId, getRequest.Id);
                    members = await _mediator.Send(query);
                    return Ok(members);
                case EDataBaseName.MSSQL:
                    // code block
                    break;
                case EDataBaseName.MYSQL:
                    query = new GetGruposFromMySQL(getRequest.PageNumber, getRequest.PageSize, getRequest.FiltraNome, getRequest.FiltroNome, getRequest.FiltraInsertedOn, getRequest.DataInicial, getRequest.DataFinal, getRequest.FiltraStatus, getRequest.Status, getRequest.FiltraPorId, getRequest.Id);
                    members = await _mediator.Send(query);
                    return Ok(members);
                case EDataBaseName.ORSQL:
                    // code block
                    break;
                case EDataBaseName.PGSQL:
                    query = new GetGruposFromPostgreSQL(getRequest.PageNumber, getRequest.PageSize, getRequest.FiltraNome, getRequest.FiltroNome, getRequest.FiltraInsertedOn, getRequest.DataInicial, getRequest.DataFinal, getRequest.FiltraStatus, getRequest.Status, getRequest.FiltraPorId, getRequest.Id);
                    members = await _mediator.Send(query);
                    return Ok(members);
                case EDataBaseName.Redis:
                    query = new GetGrupoFromNoSQL(getRequest.PageNumber, getRequest.PageSize, getRequest.FiltraNome, getRequest.FiltroNome, getRequest.FiltraInsertedOn, getRequest.DataInicial, getRequest.DataFinal, getRequest.FiltraStatus, getRequest.Status, getRequest.FiltraPorId, getRequest.Id);
                    members = await _mediator.Send(query);
                    return Ok(members);
                case EDataBaseName.SQLIT:
                    query = new GetGruposFromSQLite(getRequest.PageNumber, getRequest.PageSize, getRequest.FiltraNome, getRequest.FiltroNome, getRequest.FiltraInsertedOn, getRequest.DataInicial, getRequest.DataFinal, getRequest.FiltraStatus, getRequest.Status, getRequest.FiltraPorId, getRequest.Id);
                    members = await _mediator.Send(query);
                    return Ok(members);
                default:
                    // code block
                    break;
            }

            return BadRequest();
            
        }
        catch (Exception ex )
        {
            string sEx = ex.Message;
            return BadRequest();
        }
    }    

    [HttpPost]
    public async Task<IActionResult> CreateGrupo(CreateGrupoCommand command)
    {        
        var createdGrupo = await _mediator.Send(command);
        
        GrupoResponse grupoResponse = (GrupoResponse)createdGrupo.Data;

        return CreatedAtAction(nameof(GetGrupos), new { id = grupoResponse.Grupos[0].Id }, createdGrupo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGrupo(string id, UpdateGrupoCommand command)
    {
        Guid.TryParse(id, out Guid isIdValido);
        var updatedGrupo = await _mediator.Send(command);

        return updatedGrupo != null ? Ok(updatedGrupo) : NotFound("Grupo not found.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrupo(DeleteGrupoCommand command)
    {        
        var deletedGrupo = await _mediator.Send(command);

        return deletedGrupo != null ? Ok(deletedGrupo) : NotFound("Grupo not found.");
    }
}
