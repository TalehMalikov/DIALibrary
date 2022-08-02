using Library.Core.Result.Abstract;

namespace Library.Core.Result.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; set; }

        public DataResult(T data, string message, bool success) : base(message, success)
        {
            Data = data;
        }
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public DataResult()
        {
        }
    }
}
