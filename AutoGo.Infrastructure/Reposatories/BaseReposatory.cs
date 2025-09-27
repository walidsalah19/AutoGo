using AutoGo.Domain.Interfaces.Repo;
using AutoGo.Domain.Models;
using AutoGo.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task AddRangeAsync(List<T> entity)
        {
            await appDbContext.Set<T>().AddRangeAsync(entity);
        }

        public async Task<T> FindEntityById(string Id)
        {
            return await appDbContext.Set<T>().FindAsync(Id);
        }

        public async Task<IQueryable<T>> GetRange(Expression<Func<T, bool>> ex)
        {
           var entities=   appDbContext.Set<T>().Where(ex);
           return entities;
        }

        public async Task Remove(T entity)
        {
            appDbContext.Set<T>().Remove(entity);
        }

        public async Task RemoveRange(List<T> entities)
        {
            appDbContext.Set<T>().RemoveRange(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            appDbContext.Set<T>().Update(entity);
        }
    }
}
