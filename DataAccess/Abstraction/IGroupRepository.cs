using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.DataAccess.Abstraction
{
    public interface IGroupRepository : ICrudRepository<Group>
    {
        bool Add(GroupDto value);
        bool Update(GroupDto value);
    }
}
