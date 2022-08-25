using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Models
{
    public class AuthViewModel
    {
        public AccountLoginDto LoginModel { get; set; }

        public DataResult<List<File>> NewAddedBookList { get; set; }
    }
}
