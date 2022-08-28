using Library.Entities.Concrete;
using Library.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.WebUI.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public Student Student { get; set; }

        public bool SelectedGender { get; set; }

/*        public Faculty SelectedFaculty { get; set; }

        public SelectList FacultySelectList { get; set; }*/
    }
}
