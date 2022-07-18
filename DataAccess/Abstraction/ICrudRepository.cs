using Library.Core.Domain.Abstract;

namespace Library.DataAccess.Abstraction
{
    public interface ICrudRepository<T> where T : class, IEntity,new()
    {
        bool Add(T value);
        T Get(int id);
        List<T> GetAll();
        bool Update(T value);
        bool Delete(int id);
    }
}
