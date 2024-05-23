using Infrastructure.SQLServer.Repositories.Base.Dapper;
using System.Data;

namespace Infrastructure.SQLServer.Repositories.Member
{
    public class MemberDapperRepository : DapperRepository<Domain.Entities.Member>, IMemberDapperRepository
    {
        public MemberDapperRepository(IDbConnection dbConnection) : base(dbConnection) { }
    }
}
