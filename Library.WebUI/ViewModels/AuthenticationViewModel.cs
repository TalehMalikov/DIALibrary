using Library.Core.Domain.Dtos;
using Library.Entities.Dtos;

namespace Library.WebUI.ViewModels
{
    public class AuthenticationViewModel
    {
        public AccountLoginDto LoginModel { get; set; }

        public List<FileAuthorDto> NewAddedFileAuthorList { get; set; }
    }
}
