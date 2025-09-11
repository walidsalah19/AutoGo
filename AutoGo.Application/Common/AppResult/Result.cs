using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagement.Application.Common.Result
{
    public class Result<T> 
    {
        public T? Value { get;}
        public object Error { get; }

        public bool isSuccessed=>Error==null;

        private Result(T? value)
        {
            Value = value;
            Error = null;
        }

        private Result(Object error)
        {
            Error = error;
            Value = default;
        }

        public static Result<T> Success(T Value) =>new Result<T>(Value);
        public static Result<T> Failure(Object error) => new Result<T>(error);

        public TResult Map<TResult>(Func<T,TResult> onSuccess, Func<Object, TResult> onFailure)
        {
            return isSuccessed ? onSuccess(Value!) : onFailure(Error!);
        }

        
    }
}
