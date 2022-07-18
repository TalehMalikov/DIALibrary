using Library.Core.Domain.Abstract;

namespace Library.Entities.Concrete
{
    public class Author : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public short BookCount { get; set; }
        public bool Gender { get; set; }
    }
}
