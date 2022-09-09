using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IEducationalProgramService
    {
        Task<DataResult<List<EducationalProgram>>> GetAll(string token);
        Task<DataResult<EducationalProgram>> GetByGUID(string accessToken, string guid);
    }
}
