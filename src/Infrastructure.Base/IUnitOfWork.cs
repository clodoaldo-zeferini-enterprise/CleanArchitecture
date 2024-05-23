
namespace Infrastructure.Base;

public interface IUnitOfWork : IDisposable
{
    IMemberRepositoryCommand MemberRepositoryCommand { get; }
    Task<int> Save();
}