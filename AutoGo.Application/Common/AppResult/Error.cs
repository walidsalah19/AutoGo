using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Result
{
   public class Error
    {
      public  string message { get; }
      public string code { get; }

        public Error(string message, string code)
        {
            this.message = message;
            this.code = code;
        }
    }
}
