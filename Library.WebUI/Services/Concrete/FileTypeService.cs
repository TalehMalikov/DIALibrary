using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class FileTypeService : BaseService, IFileTypeService
    {
        public async Task<DataResult<List<FileType>>> GetAllFileTypes()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<FileType>>>(BaseUrl + "FileType/getall");
            return result;
        }
    }
}
