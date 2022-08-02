using Library.Core.Result.Abstract;

namespace Library.Core.Result.Concrete
{
    public class SuccessResult : Result, IResult
    {
        public SuccessResult(string message) : base(message, true)
        {

        }
        public SuccessResult() : base(true)
        {

        }
    }
}
