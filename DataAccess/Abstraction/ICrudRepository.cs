using Library.Entities.Abstract;

namespace Library.DataAccess.Abstraction
{
    public interface ICrudRepository<T> where T : BaseEntity, new()
    {
        
        T Get(int id);
        List<T> GetAll();
        
        bool Delete(int id);
    }
}
