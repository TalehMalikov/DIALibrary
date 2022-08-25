using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Models
{
    public class AuthViewModel
    {
        public AccountLoginDto LoginModel { get; set; }

        public DataResult<List<Publication>> NewAddedBookList { get; set; }
    }
}
