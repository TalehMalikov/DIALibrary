using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class ExternalSource : BaseEntity
    {
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string SourceLink { get; set; }
    }
}
