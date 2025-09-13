using AutoGo.Domain.Interfaces.Repo;
using AutoGo.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Reposatories
{
    public class BaseReposatory<T> : IBaseReposatory<T> where T : class
    {
        private readonly AppDbContext appDbContext;

        public BaseReposatory(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddAsync(T entity)
        {
            await appDbContext.Set<T>().AddAsync(entity);
        }

        public Task<T> GetEntityById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
