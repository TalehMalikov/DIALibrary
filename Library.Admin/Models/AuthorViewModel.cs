using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }
        public List<Author> Authors { get; set; }

        //public MultiSelectList AuthorList { get; set; }
        public List<SelectListItem> AuthorList { get; set; } = new List<SelectListItem>();
        public int[] SelectedValues { get; set; } 

        public FileAuthorDto FileAuthor { get; set; }
        public FileAuthorDtoForCrud FileAuthorForCrud { get; set; }
    }
}
