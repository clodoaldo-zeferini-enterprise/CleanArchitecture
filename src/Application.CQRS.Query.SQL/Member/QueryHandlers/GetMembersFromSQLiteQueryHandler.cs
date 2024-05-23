using Application.CQRS.Query.SQL.Member.Queries;
using Application.DTO.Response;
using Application.DTO.Response.Member;
using Infrastructure.SQLServer.Repositories.Base.Dapper;
using Infrastructure.SQLServer.Repositories.Member;
using Infrastructure.SQLServer.Service.Member;
using MediatR;

namespace Application.CQRS.Query.SQL.Member.QueryHandlers;

public class GetMembersFromSQLiteQueryHandler : IRequestHandler<GetMemberFromSQLite, MemberOutResponse>
{
    private MemberOutResponse _memberOutResponse;
    private MemberResponse response;

    private readonly IMemberDapperRepository _memberDapperRepository;

    public GetMembersFromSQLiteQueryHandler(IMemberDapperRepository memberDapperRepository)
    {
        _memberDapperRepository = memberDapperRepository;
        _memberOutResponse =  new MemberOutResponse();
    }
    public async Task<MemberOutResponse> Handle(GetMemberFromSQLite request, CancellationToken cancellationToken)
    {
        var members = await _memberDapperRepository.Get(new DapperQuery(request.SysUsuSessionId, request.Select));

        if (members.Any())
        {
            _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(members));

            _memberOutResponse.SetResultado(true);
            _memberOutResponse.AddMensagem("Dados retornados com sucesso!");
        }
        else
        {
            _memberOutResponse.SetResultado(true);
            _memberOutResponse.AddMensagem("Nenhum dado encontrado!");
        }

        return _memberOutResponse;
    }
}


