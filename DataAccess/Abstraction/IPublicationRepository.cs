using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IPublicationRepository : ICrudRepository<Publication>
    {
        List<Publication> GetNewAdded();
    }
}
