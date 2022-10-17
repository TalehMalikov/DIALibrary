using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IUserRepository :ICrudRepository<User>
    {
        bool Add(User value);
        bool Update(User value);
        int AddAsStudent(User student);
        bool DeleteFromDb(int id); 
    }
}
