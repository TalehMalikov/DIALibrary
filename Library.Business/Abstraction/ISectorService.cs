using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface ISectorService : IBaseService<Sector>
    {
        Result Add(Sector value);
        Result Update(Sector value);
    }
}
