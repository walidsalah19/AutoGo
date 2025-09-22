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
        public async Task Remove(T entity)
        {
            var user =  appDbContext.Set<T>().Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            appDbContext.Set<T>().Update(entity);
        }
    }
}
