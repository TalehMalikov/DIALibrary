using Library.Entities.Concrete;
using Library.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.WebUI.ViewModels
{
    public class AccountViewModel
    {
        //public AccountModel Account { get; set; }
        public Account Account { get; set; }
        public Student Student { get; set; }

        public Faculty SelectedFaculty { get; set; }
        public Specialty SelectedSpecialty { get; set; }
        public Sector SelectedSector { get; set; }


        public SelectList FacultySelectList { get; set; }
        public SelectList SpecialtySelectList { get; set; }
        public SelectList SectorSelectList { get; set; }
    }
}
