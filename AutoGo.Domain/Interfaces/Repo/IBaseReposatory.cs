using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Domain.Models;

namespace AutoGo.Domain.Interfaces.Repo
{
    public interface IBaseReposatory<T> where T:class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);

        Task Remove(T entity);
        Task RemoveRange(List<T> entities);
        Task<IQueryable<T>> GetRange(Expression<Func<T, bool>> ex);

        Task UpdateAsync(T entity);
        Task<T> FindEntityById(string Id);

    }
}
