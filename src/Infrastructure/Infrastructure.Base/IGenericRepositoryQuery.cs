using Application.DTO.Request;

namespace Infrastructure.Base
{
    public interface IGenericRepositoryQuery<T> where T : class
    {
        Task<T> Get(GetFrom getFrom);
    }
}
