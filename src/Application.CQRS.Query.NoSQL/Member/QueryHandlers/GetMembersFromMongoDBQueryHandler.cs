using Application.CQRS.Base.Members.Notifications;
using Application.CQRS.Query.NoSQL.Member.Queries;

using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Member;
using MediatR;

namespace Application.CQRS.Query.NoSQL.Member.QueryHandlers;

public class GetMembersFromMongoDBQueryHandler : IRequestHandler<GetMemberFromMongoDB, MemberOutResponse>
{
    private readonly IMemberServiceQuery _memberServiceQuery;
    private readonly IMediator _mediator;

    public GetMembersFromMongoDBQueryHandler(IMemberServiceQuery memberServiceQuery, IMediator mediator)
    {
        _memberServiceQuery = memberServiceQuery;
        _mediator = mediator;
    }
    public async Task<MemberOutResponse> Handle(GetMemberFromMongoDB request, CancellationToken cancellationToken)
    {
        var result = await _memberServiceQuery.GetMembers(request);

        await _mediator.Publish(new MemberNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.CREATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_CREATE), cancellationToken);

        return result;
    }
}


