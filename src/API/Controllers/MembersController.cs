using Application.CQRS.Command.Members.Commands;
using Application.CQRS.Query.SQL.Member.Queries;
using Application.DTO.Request;
using Application.DTO.Request.Member;
using Application.DTO.Response.Member;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    public MembersController(IConfiguration configuration, IMediator mediator)
    {
        _mediator = mediator;
       // _logger = logger;
       _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers([FromQuery] GetRequest getRequest)
    {
        try
        {
            //_logger.LogInformation("Estou no Index do HomeController");
            var dbServer = _configuration.GetValue<string>("DBServer");
            Object query;
            Object members;

            switch (dbServer)
            {
                case "MSSQL":
                    // code block
                    break;
                case "MYSQL":
                    // code block
                    break;
                case "ORSQL":
                    // code block
                    break;
                case "PGSQL":
                    // code block
                    break;
                case "SQLIT":
                    query = new GetMemberFromSQLite(1, 10, false, "", false, null, null, true, Domain.Enums.EStatus.ATIVO);
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
    public async Task<IActionResult> CreateMember(CreateMemberRequest createMemberRequest)
    {        
        CreateMemberCommand command = new CreateMemberCommand(createMemberRequest.UserId, createMemberRequest.FirstName, createMemberRequest.LastName, createMemberRequest.Gender, createMemberRequest.Email);
        var createdMember = await _mediator.Send(command);
        
        MemberResponse memberResponse = (MemberResponse)createdMember.Data;

        return CreatedAtAction(nameof(GetMembers), new { id = memberResponse.Members[0].Id }, createdMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(string id, UpdateMemberCommand command)
    {
        Guid.TryParse(id, out Guid isIdValido);
        var updatedMember = await _mediator.Send(command);

        return updatedMember != null ? Ok(updatedMember) : NotFound("Member not found.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(DeleteMemberCommand command)
    {        
        var deleteCommand = new DeleteMemberCommand(command.Id, command.UserId, command.FirstName, command.LastName, command.Gender, command.Email, command.Status);
        var deletedMember = await _mediator.Send(command);

        return deletedMember != null ? Ok(deletedMember) : NotFound("Member not found.");
    }
}
