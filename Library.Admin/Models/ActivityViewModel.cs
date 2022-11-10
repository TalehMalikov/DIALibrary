using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class ActivityViewModel
    {
        public Activity Activity { get; set; }
        public List<Activity> Activities { get; set; }
        public IFormFile ActivityPhoto { get; set; }
    }
}
