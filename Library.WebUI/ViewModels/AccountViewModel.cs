using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.WebUI.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; } = new Account();
        public Student Student { get; set; }

        public bool SelectedGender { get; set; }

        public EducationalProgramViewModel EducationalProgramViewModel { get; set; }
    }
}
