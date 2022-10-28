using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
