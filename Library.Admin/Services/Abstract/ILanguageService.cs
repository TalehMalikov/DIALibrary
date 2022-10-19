using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface ILanguageService 
    {
        Task<DataResult<List<Language>>> GetAll(string token);
        Task<DataResult<Language>> Get(string token,int id);
    }
}
