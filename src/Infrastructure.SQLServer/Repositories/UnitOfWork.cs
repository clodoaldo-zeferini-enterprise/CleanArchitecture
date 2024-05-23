using Infrastructure.Base;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Repositories.Member;

namespace Infrastructure.SQLServer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IMemberRepositoryCommand? _memberRepositoryCommand;
        public AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IMemberRepositoryCommand MemberRepositoryCommand { get { return _memberRepositoryCommand ?? new MemberRepositoryCommand(_appDbContext); } }

        public Task<int> Save()
        {
            return _appDbContext.SaveChangesAsync();
        }

        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _appDbContext = null;    
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion

    }
}
