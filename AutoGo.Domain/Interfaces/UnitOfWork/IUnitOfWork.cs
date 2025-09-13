using AutoGo.Domain.Interfaces.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Interfaces.UnitofWork
{
    public interface IUnitOfWork :IDisposable
    {
        IBaseReposatory<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();

    }
}
