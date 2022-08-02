namespace Library.Core.Result.Abstract
{
    public interface IResult
    {
        string Message { get; set; }
        bool Success { get; set; }
    }
}
