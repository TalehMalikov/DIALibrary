using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class GroupViewModel
    {
        public Group Group { get; set; }
        public  List<Group> Groups { get; set; }
        public SelectList SectorList { get; set; }
        public SelectList SpecialtyList { get; set; }
    }
}
