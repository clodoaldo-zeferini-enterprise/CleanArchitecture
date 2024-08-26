using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.SQLServer.Repositories.Base.Dapper
{
    public abstract class DapperRepository<TEntity> : IRepositorySQL<TEntity> where TEntity : class
    {
        private readonly IDbConnection _dbConnection;

        protected DapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(DapperQuery dapperQuery, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2)
        {
            var objs = ObterMultiple(dapperQuery, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(DapperQuery dapperQuery, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3)
        {
            var objs = ObterMultiple(dapperQuery, parameters, func1, func2, func3);
            return Tuple.Create(
                  objs[0] as IEnumerable<T1>
                , objs[1] as IEnumerable<T2>
                , objs[2] as IEnumerable<T3>);
        }

        private List<object> ObterMultiple(DapperQuery dapperQuery, object parameters, params Func<GridReader, object>[] readerFuncs)
        {
            var returnResults = new List<object>();
            try
            {
                var gridReader = _dbConnection.QueryMultiple(dapperQuery.Query, parameters);

                foreach (var readerFunc in readerFuncs)
                {
                    var obj = readerFunc(gridReader);
                    returnResults.Add(obj);
                }
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
            }
            finally
            {
            }

            return returnResults;
        }

        public virtual async Task<IEnumerable<TEntity>> Get(DapperQuery dapperQuery)
        {
            try
            {
                return await _dbConnection.QueryAsync<TEntity>(dapperQuery.Query);
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return null;
            }
            finally
            {
            }
        }

        public virtual async Task<int> ExecuteCommand(DapperQuery dapperQuery)
        {
            try
            {
                var resultado = await _dbConnection.ExecuteAsync(dapperQuery.Query);
                return resultado;
            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
                return -1;
            }
            finally
            {
            }
        }

        Task<IEnumerable<TEntity>> IRepositorySQL<TEntity>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IRepositorySQL<TEntity>.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}