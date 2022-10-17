using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface ILanguageRepository : ICrudRepository<Language>
    {
        bool Add(Language value);
        bool Update(Language value);
    }
}
