using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class SpecialtyViewModel
    {
        public Specialty Specialty { get; set; }
        public SpecialtyDto SpecialtyDto { get; set; }

        public SelectList FacultyList { get; set; }

        public List<Specialty> SpecialtyList { get; set; }
    }
}
