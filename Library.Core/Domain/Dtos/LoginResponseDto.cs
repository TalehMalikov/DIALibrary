namespace Library.Core.Domain.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
