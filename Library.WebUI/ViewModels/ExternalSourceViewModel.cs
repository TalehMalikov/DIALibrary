using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Models
{
    public class ExternalSourceViewModel
    {
        public DataResult<List<ExternalSource>> ExternalSourceList { get; set; }
    }
}
