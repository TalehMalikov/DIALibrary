using Library.Entities.Concrete;

namespace Library.WebAPI.Models
{
    public class RegisterRequestModel
    {
        public int UserId { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
