using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class SpecialtyViewModel
    {
        public Specialty Specialty { get; set; }

        public SelectList FacultyList { get; set; }

        public List<Specialty> SpecialtyList { get; set; }
    }
}
