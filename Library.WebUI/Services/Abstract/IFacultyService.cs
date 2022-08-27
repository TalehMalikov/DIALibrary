using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IFacultyService
    {
        Task<DataResult<List<Faculty>>> GetAllFaculties();
    }
}
