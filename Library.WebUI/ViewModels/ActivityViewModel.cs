using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.ViewModels
{
    public class ActivityViewModel
    {
        public DataResult<List<Activity>> Activities { get; set; }
        public DataResult<Activity> Activity { get; set; }
    }
}
