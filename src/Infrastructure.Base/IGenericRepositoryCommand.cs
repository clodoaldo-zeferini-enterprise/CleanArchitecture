namespace Infrastructure.Base
{
    public interface IGenericRepositoryCommand<T> where T : class
    {
        Task<T> GetById(string id);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
