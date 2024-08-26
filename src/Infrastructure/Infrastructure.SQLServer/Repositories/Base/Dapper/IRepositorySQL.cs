using static Dapper.SqlMapper;

namespace Infrastructure.SQLServer.Repositories.Base.Dapper
{
    public interface IRepositorySQL<TEntity> where TEntity : class
    {
        Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(DapperQuery dapperQuery, object parameters,
            Func<GridReader, IEnumerable<T1>> func1, Func<GridReader, IEnumerable<T2>> func2);

        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(DapperQuery dapperQuery, object parameters,
            Func<GridReader, IEnumerable<T1>> func1,
            Func<GridReader, IEnumerable<T2>> func2,
            Func<GridReader, IEnumerable<T3>> func3);

        Task<IEnumerable<TEntity>> Get(DapperQuery dapperQuery);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(string id);
        Task<int> ExecuteCommand(DapperQuery dapperQuery);


    }
}
