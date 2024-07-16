using Infrastructure.Base;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Repositories.Base.EF;

namespace Infrastructure.SQLServer.Repositories.Grupo
{
    public class GrupoRepositoryCommand : GenericSQLRepository<Domain.Entities.Grupo>, IGrupoRepositoryCommand
    {
        public GrupoRepositoryCommand(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
