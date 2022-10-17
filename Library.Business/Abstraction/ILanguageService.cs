using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface ILanguageService : IBaseService<Language>
    {
        Result Add(Language value);
        Result Update(Language value);
    }
}
