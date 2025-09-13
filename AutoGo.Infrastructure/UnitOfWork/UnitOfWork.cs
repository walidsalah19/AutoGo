using AutoGo.Domain.Interfaces.Repo;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Infrastructure.Data.Context;
using AutoGo.Infrastructure.Reposatories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories = new();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBaseReposatory<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            // ✅ نعمل Cache للـ Repositories عشان لو اتطلبت أكتر من مرة
            if (_repositories.ContainsKey(type))
                return (IBaseReposatory<T>)_repositories[type];

            var repositoryInstance = new BaseReposatory<T>(_context);
            _repositories.TryAdd(type, repositoryInstance);

            return repositoryInstance;
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
