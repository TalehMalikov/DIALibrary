using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IExternalSourceService
    {
        Task<Result> Add(string token, ExternalSource entity);
        Task<Result> Update(string token, ExternalSource entity);
        Task<Result> Delete(string token,int id);
        Task<DataResult<List<ExternalSource>>> GetAll();
        Task<DataResult<ExternalSource>> Get(string token, int id);

    }
}
