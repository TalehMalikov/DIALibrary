using Library.Core.Result.Concrete;

namespace FileTester.Services.Abstract
{
    public interface IPublicationService
    {
        Task<DataResult<List<Publication>>> GetAll();
        Task<DataResult<List<Publication>>> GetNewAddedBooks(int count);
        Task<DataResult<Publication>> Get(int id);
        Task<Result> Add(Publication publication);
        Task<Result> Delete(int id);
    }
}
