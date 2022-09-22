using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Result.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class BaseService
    {
        protected const string BaseUrl = "https://localhost:7185/api/";
    }
}
