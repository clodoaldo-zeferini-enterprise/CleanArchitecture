using Infrastructure.SQLServer.Context;

namespace Infrastructure.SQLServer.Repositories.Base.EF
{
    public abstract class GenericSQLRepository<T> : Infrastructure.Base.IGenericRepositoryCommand<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        protected GenericSQLRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> GetById(string id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /*
        public async Task<IEnumerable<T>> Get(GetFrom getFrom)
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        */


        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }

}
