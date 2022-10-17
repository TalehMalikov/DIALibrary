using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface IGroupService : IBaseService<Group>
    {
        Result Add(GroupDto value);
        Result Update(GroupDto value);
    }
}
