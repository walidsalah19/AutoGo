using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Enums
{
    public enum ErrorCodes
    {
        NotFound = 404,
        AlreadyExists = 409,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        InternalServerError = 500
    }
}
