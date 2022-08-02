using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Account : BaseEntity
    {
        public User User { get; set; }
        public string AccountName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModified { get; set; }
    }
}
