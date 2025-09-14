using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Pagination
{
    public class PageParameters
    {
        public required int PageNumber { get; set; } = 1;

        public required int Pagesize { get; set; } = 10;

    }
}
