using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class AccountRole : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }
    }
}
