
namespace Infrastructure.Base;

public interface IUnitOfWork : IDisposable
{
    IGrupoRepositoryCommand GrupoRepositoryCommand { get; }
    Task<int> Save();
}