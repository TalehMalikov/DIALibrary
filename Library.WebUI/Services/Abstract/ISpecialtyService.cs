using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface ISpecialtyService
    {
        Task<DataResult<List<Specialty>>> GetAllSpecialties();
    }
}
