using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {

        Task<int> CompleteAsync();

    }
}
