namespace Infrastructure.SQLServer.Repositories.Base.Dapper
{
    public class DapperQuery
    {
        public string SysUsuSessionId { get; private set; }
        public string Query { get; private set; }

        public DapperQuery(string sysUsuSessionId, string query)
        {
            SysUsuSessionId = sysUsuSessionId;
            Query = query;
        }
    }
}
