using Application.DTO.Request;
using Infrastructure.Base;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Repositories.Base.EF;

namespace Infrastructure.SQLServer.Repositories.Member
{
    public class MemberRepositoryCommand : GenericSQLRepository<Domain.Entities.Member>, IMemberRepositoryCommand
    {
        public MemberRepositoryCommand(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
