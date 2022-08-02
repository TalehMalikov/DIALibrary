using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public Faculty Faculty { get; set; }
    }
}
