using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface ISectorService 
    {
        Task<Result> Add(string token, Sector entity);
        Task<Result> Update(string token, Sector entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Sector>>> GetAll(string token);
    }
}
