using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.ViewModels
{
    public class ExternalSourceViewModel
    {
        public DataResult<List<ExternalSource>> ExternalSourceList { get; set; }
    }
}
