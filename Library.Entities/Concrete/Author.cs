using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public short BookCount { get; set; }
        public bool Gender { get; set; }
        public bool IsInstitution { get; set; }
    }
}
