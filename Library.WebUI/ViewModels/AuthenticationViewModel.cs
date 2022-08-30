using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Models
{
    public class AuthenticationViewModel
    {
        public AccountLoginDto LoginModel { get; set; }

        public DataResult<List<File>> NewAddedBookList { get; set; }

        public DataResult<List<FileType>> AllFileTypes { get; set; }
    }
}
