using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IAuthorService : IBaseService<Author>
    {
        Result Add(Author value);
        Result Update(Author value);
    }
}
