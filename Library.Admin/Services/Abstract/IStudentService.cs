using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface IStudentService
    {
        Task<Result> Add(string token, StudentDto entity);
        Task<Result> Update(string token, Student entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Student>>> GetAll(string token);
    }
}
