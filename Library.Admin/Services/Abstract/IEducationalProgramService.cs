using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface IEducationalProgramService
    {
        Task<Result> Add(string token, EducationalProgramDto educationalProgramDto);
        Task<Result> Update(string token, EducationalProgramDto educationalProgramDto);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<EducationalProgram>>> GetAll(string token);
        Task<DataResult<EducationalProgram>> Get(string token,int id);
    }
}
