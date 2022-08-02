namespace Library.Core.Result.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; set; }
    }
}
