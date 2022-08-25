using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IPublicationService : IBaseService<Publication>
    {
        DataResult<List<Publication>> GetNewAdded();
    }
}
