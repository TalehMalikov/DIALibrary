using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class SpecialtyViewModel
    {
        public SpecialtyDto Specialty { get; set; }

        public SelectList FacultyList { get; set; }

        public List<Specialty> SpecialtyList { get; set; }
    }
}
