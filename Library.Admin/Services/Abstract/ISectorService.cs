using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface ISectorService : IBaseService<Sector>
    {
        Task<DataResult<List<Sector>>> GetAll(string token);
    }
}
