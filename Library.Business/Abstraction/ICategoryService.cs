using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface ICategoryService : IBaseService<Category>
    {

        Result Add(Category value);
        Result Update(Category value);
    }
}
