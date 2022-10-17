using Library.Core.Result.Concrete;

namespace Library.Business.Abstraction
{
    public interface IBaseService<T>
    {
        Result Delete(int id);
        DataResult<T> Get(int id);
        DataResult<List<T>> GetAll();
    }
}
