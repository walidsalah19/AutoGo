using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.ActivationUsers.SpecificPeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.Jops
{
    public interface IBackgroundJops
    {
        public Task DeactivateForSpecificPeriod(DeactivateForSpecificPeriod period, Domain.Models.ApplicationUser user);
    }
}
