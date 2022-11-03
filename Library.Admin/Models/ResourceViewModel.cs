using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using File = Library.Entities.Concrete.File;
namespace Library.Admin.Models
{
    public class ResourceViewModel
    {
        public List<File> Files { get; set; }
        public List<EducationalProgram> EducationalPrograms { get; set; }

        public IFormFile AddedPicture { get; set; }
        public IFormFile AddedFile { get; set; }


        public SelectList CategoryList { get; set; }
        public SelectList LanguageList { get; set; }
        public SelectList FileTypeList { get; set; }
        public SelectList EducationLevelList { get; set; }  //
        public SelectList SpecialtyList { get; set; }  //

        #region Author
        //public int[] Members { get; set; }
        public List<int> Members { get; set; }

        public string Selectedmembers { get; set; } //used to store the selected members, such as: "tom,johnn,david"
        public SelectList AuthorList { get; set; }
        #endregion

        public File DeletedResource { get; set; }
        public File File { get; set; } = new File();

        public EducationalProgram DeletedEducationalProgram { get; set; }
        public EducationalProgram EducationalProgram { get; set; } = new EducationalProgram();
    }
}
