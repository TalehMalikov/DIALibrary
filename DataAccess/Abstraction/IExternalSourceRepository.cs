using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IExternalSourceRepository : ICrudRepository<ExternalSource>
    {
        bool Add(ExternalSource value);
        bool Update(ExternalSource value);
    }
}
