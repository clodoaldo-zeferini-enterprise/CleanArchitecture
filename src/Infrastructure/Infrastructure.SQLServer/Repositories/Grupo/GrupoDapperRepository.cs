using Infrastructure.SQLServer.Repositories.Base.Dapper;
using System.Data;

namespace Infrastructure.SQLServer.Repositories.Grupo
{
    public class GrupoDapperRepository : DapperRepository<Domain.Entities.Grupo>, IGrupoDapperRepository
    {
        public GrupoDapperRepository(IDbConnection dbConnection) : base(dbConnection) { }
    }
}
