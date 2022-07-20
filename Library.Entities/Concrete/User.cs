using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModified { get; set; }
    }
}
