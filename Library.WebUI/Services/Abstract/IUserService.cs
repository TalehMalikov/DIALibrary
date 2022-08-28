using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IUserService
    {
        Task<Result> Update(string accessToken, User user);
    }
}
