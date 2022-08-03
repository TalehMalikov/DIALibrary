using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class AccountRole : BaseEntity
    {
        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
