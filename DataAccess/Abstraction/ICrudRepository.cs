using Library.Entities.Abstract;

namespace Library.DataAccess.Abstraction
{
    public interface ICrudRepository<T> where T : BaseEntity,new()
    {
        bool Add(T value);
        T Get(int id);
        List<T> GetAll();
        bool Update(T value);
        bool Delete(int id);
    }
}
