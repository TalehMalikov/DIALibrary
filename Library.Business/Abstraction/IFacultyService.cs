using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IFacultyService : IBaseService<Faculty>
    {
        Result Add(Faculty value);
        Result Update(Faculty value);
    }
}
