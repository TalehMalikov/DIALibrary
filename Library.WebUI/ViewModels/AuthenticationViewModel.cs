using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Models
{
    public class AuthenticationViewModel
    {
        public AccountLoginDto LoginModel { get; set; }

        public List<FileAuthorDto> NewAddedFileAuthorList { get; set; }
    }
}
