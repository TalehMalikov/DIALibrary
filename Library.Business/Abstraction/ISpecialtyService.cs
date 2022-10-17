using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface ISpecialtyService : IBaseService<Specialty>
    {
        Result Add(SpecialtyDto value);
        Result Update(SpecialtyDto value);
    }
}
