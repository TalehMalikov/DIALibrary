using FileTester.Models;
using FileTester.MySystem;
using FileTester.Services.Abstract;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FileTester.Controllers
{
    public class FileController : Controller
    {
        private readonly IPublicationService _publicationService;

        public FileController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        private async Task<FileUploadViewModel> LoadAllFiles()
        {
            var viewModel = new FileUploadViewModel();
            var result = await _publicationService.GetAll();
            viewModel.Publications =  result.Data;
            return viewModel;
        }

        public async Task<IActionResult> Index()
        {
            var fileuploadViewModel = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(fileuploadViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                //var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(Defaults.DefaultPhotoPath);
                if (!basePathExists) Directory.CreateDirectory(Defaults.DefaultPhotoPath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);

                var filePath = Path.Combine(Defaults.DefaultPhotoPath, file.FileName);

                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    var publication = new Publication()
                    {
                        Id = 0,
                        Book = new Library.Entities.Concrete.File()
                        {
                            Id = 0,
                            Name = "Anonymous book",
                            Category = new Category()
                            {
                                Id = 1,
                                Name = "Tarix"
                            },
                            OriginalLanguage = new Language()
                            {
                                Id = 1,
                                Name = "Azərbaycan"
                            },
                            LastModified = DateTime.Now,
                            IsDeleted = false,
                            Status = false,
                            Type = new FileType()
                            {
                                Id = 1,
                                Name = "Kitab"
                            }
                        },
                        PublisherName = "xxxx",
                        PublicationLanguage = new Language()
                        {
                            Id = 1,
                            Name = "Azərbaycan"
                        },
                        PublicationDate = DateTime.Now,
                        PageNumber = 200,
                        LastModified = DateTime.Now,
                        IsDeleted = false,
                        Photo = filePath,
                        File = ""
                    };
                    await _publicationService.Add(publication);
                }
            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {
            var result = await _publicationService.GetAll();
            var data = result.Data.Where(x => x.Id == id).FirstOrDefault();
            var filePath = Path.Combine(Defaults.DefaultPhotoPath, data.Photo);

            if (data == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory,"img/*", data.Book.Name + data.Book.Category.Name+".jpg");
        }

        public async Task<IActionResult> DeleteFileFromFileSystem(int id)
        {

            var result = await _publicationService.GetAll();
            var data = result.Data.Where(x => x.Id == id).FirstOrDefault();

            var filePath = Path.Combine(Defaults.DefaultPhotoPath, data.Photo);


            if (data == null) return null;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            await _publicationService.Delete(data.Id);
            TempData["Message"] = $"Removed {data.Book.Name + data.Book.Category.Name} successfully from File System.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Picture(string name)
        {
            try
            {
                string fullpath = Path.Combine(Defaults.DefaultPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(name);

                return File(content, "img/*");
            }
            catch
            {
                return Ok();
            }
        }

    }
}
