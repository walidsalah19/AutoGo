using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Interfaces.Repo
{
    public interface IBaseReposatory<T> where T:class
    {
        Task AddAsync(T entity);
        Task Remove(string Id);

        Task UpdateAsync(T entity);

        Task<T> GetEntityById(String id);
    }
}
