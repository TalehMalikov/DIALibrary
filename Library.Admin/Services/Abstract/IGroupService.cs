using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IGroupService : IBaseService<Group>
    {
        Task<DataResult<List<Group>>> GetAll(string token);
        Task<DataResult<Group>> Get(string accessToken, int id);
    }
}
