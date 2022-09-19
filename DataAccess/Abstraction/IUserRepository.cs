using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IUserRepository :ICrudRepository<User>
    {
        int AddAsStudent(User student);
    }
}
