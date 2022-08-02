using Library.Core.Result.Abstract;

namespace Library.Core.Result.Concrete
{
    public class Result : IResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public Result(string message, bool success) : this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public Result()
        {
        }
    }
}
