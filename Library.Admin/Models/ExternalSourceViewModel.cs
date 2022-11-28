using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class ExternalSourceViewModel
    {
        public ExternalSource Source { get; set; }
        public List<ExternalSource> SourceList { get; set; }
        public IFormFile Photo { get; set; }
    }
}
