using Application.CQRS.Query.SQL.Member.Queries;
using Application.DTO.Response;
using Application.DTO.Response.Member;
using Infrastructure.SQLServer.Repositories.Base.Dapper;
using Infrastructure.SQLServer.Repositories.Member;
using MediatR;

namespace Application.CQRS.Query.SQL.Member.QueryHandlers;


public class GetMembersFromSQLServerQueryHandler : IRequestHandler<GetMemberFromSQLServer, MemberOutResponse>
{
    private MemberOutResponse _memberOutResponse;
    private MemberResponse response;

    private readonly IMemberDapperRepository _memberDapperRepository;

    public GetMembersFromSQLServerQueryHandler(IMemberDapperRepository memberDapperRepository)
    {
        _memberDapperRepository = memberDapperRepository;
        _memberOutResponse =  new MemberOutResponse();
    }
    public async Task<MemberOutResponse> Handle(GetMemberFromSQLServer request, CancellationToken cancellationToken)
    {
        var navigatorMembers = _memberDapperRepository.GetMultiple(new DapperQuery(Guid.NewGuid().ToString(), request.Select), new { param = "" },
                            gr => gr.Read<Navigator>()
                          , gr => gr.Read<Domain.Entities.Member>()

                );

        var navigators = navigatorMembers.Item1;
        var members = navigatorMembers.Item2;

        List<Navigator> responseNavigators = new List<Navigator>();
        foreach (Navigator navigator in navigators)
        {
            responseNavigators.Add(new Navigator(navigator.RecordCount, navigator.PageNumber, navigator.PageSize, navigator.PageCount));
        }

        List<Domain.Entities.Member> responseMembers = new List<Domain.Entities.Member>();
        foreach (Domain.Entities.Member member in members)
        {
            responseMembers.Add(new Domain.Entities.Member(member.Id, member.FirstName, member.LastName, member.Gender, member.Email, member.Status, member.InsertedOn, member.InsertedBy, member.UpdatedOn, member.UpdatedBy));
        }

        if (responseNavigators.Any() && responseMembers.Any())
        {
            //var responseMember = new Infrastructure.SQLServer.Service.Member.MemberMapper.FromSQLDBToDTO(responseMembers);

            _memberOutResponse.SetResultado(true);
            _memberOutResponse.AddMensagem("Dados retornados com sucesso!");
            _memberOutResponse.SetData(response);
        }
        else
        {
            _memberOutResponse.SetResultado(true);
            _memberOutResponse.AddMensagem("Nenhum dado encontrado!");
        }

        return _memberOutResponse;
    }
}


