using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class GroupViewModel
    {
        public GroupDto GroupDto { get; set; }
        public Group Group { get; set; }
        public  List<Group> Groups { get; set; }
        public SelectList SectorList { get; set; }
        public SelectList SpecialtyList { get; set; }
    }
}
