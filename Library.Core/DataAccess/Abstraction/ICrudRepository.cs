namespace Library.Core.DataAccess.Abstraction
{
    public interface ICrudRepository<T>
    {
        bool Add(T value);
        T Get(int id);
        List<T> GetAll();
        bool Update(T value);
        bool Delete(int id);
    }
}
