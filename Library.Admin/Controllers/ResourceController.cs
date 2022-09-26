using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.DefaultSystemPath;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using File = Library.Entities.Concrete.File;

namespace Library.Admin.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ILanguageService _languageService;
        private readonly ICategoryService _categoryService;
        public ResourceController(IFileService fileService, ILanguageService languageService, ICategoryService categoryService)
        {
            _fileService = fileService;
            _languageService = languageService;
            _categoryService = categoryService;
        }

        #region Books

        public async Task<IActionResult> ShowBooks(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            ResourceViewModel viewModel = new ResourceViewModel();
            var result = await _fileService.GetAll();
            switch (id)
            {
                case 0:
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == false).ToList();
                    break;
                case 1:
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == true).ToList();
                    break;
                default:
                    viewModel.Files = result.Data;
                    break;
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SaveBook(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            ResourceViewModel viewModel = new ResourceViewModel();
            var categories = await _categoryService.GetAll();
            categories.Data.Add(new Category()
            {
                Name = "Seç",
                Id = 0
            });
            var languages = await _languageService.GetAll(accessToken);
            languages.Data.Add(new Language()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.CategoryList = new SelectList(categories.Data, "Id", "Name", "Seç");
            viewModel.LanguageList = new SelectList(languages.Data, "Id", "Name", "Seç");

            List<dynamic> editions = new List<dynamic>();
            editions.Add(new { Id = 0, Name = "true" });
            editions.Add(new { Id = 0, Name = "false" });
            viewModel.EditionList = new SelectList(editions, "Id", "Name");

            if (id == 0)
                return PartialView(viewModel);

            var file = await _fileService.Get(id);

            viewModel.File = file.Data;
            viewModel.OriginalLanguageId = viewModel.File.OriginalLanguage.Id;
            viewModel.PublicationLanguageId = viewModel.File.PublicationLanguage.Id;
            viewModel.CategoryId = viewModel.File.Category.Id;
            viewModel.EditionStatusId = viewModel.File.EditionStatus ? 1 : 0;

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBook(ResourceViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            try
            {
                /*if (ModelState.IsValid == false)
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                    var errorMessage = errors.Aggregate((message, value) =>
                    {
                        if (message.Length == 0)
                            return value;

                        return message + ", " + value;
                    });

                    TempData["Message"] = errorMessage;
                    return RedirectToAction("ShowBooks");
                }*/

                var categories = await _categoryService.GetAll();
                viewModel.File.Category = categories.Data.FirstOrDefault(c => c.Id == viewModel.CategoryId);

                var languages = await _languageService.GetAll(accessToken);
                viewModel.File.OriginalLanguage = languages.Data.FirstOrDefault(l => l.Id == viewModel.OriginalLanguageId);
                viewModel.File.PublicationLanguage = languages.Data.FirstOrDefault(l => l.Id == viewModel.PublicationLanguageId);

                if (viewModel.EditionStatusId == 0)
                    viewModel.File.EditionStatus = false;
                else
                    viewModel.File.EditionStatus = true;

                if (viewModel.File.Id == 0)
                {
                    viewModel.File.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);
                    viewModel.File.GUID = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.File.Name);
                    viewModel.File.PhotoPath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedPicture.FileName);

                    var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.File.FilePath);
                    var uploadPhoto = await UploadToFileSystem(viewModel.AddedPicture, viewModel.File.PhotoPath);

                    if(uploadFile.Success&& uploadPhoto.Success)
                    {
                        var result = await _fileService.Add(accessToken, viewModel.File);

                        if (!result.Success)
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath,viewModel.File.FilePath));
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
                        }
                    }
                    else
                    {
                        if(!uploadFile.Success)
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.File.FilePath));
                        }
                        if(!uploadPhoto.Success)
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
                        }
                        TempData["Message"] = "File isn't upload!";
                    }
                }
                else
                {
                    string oldPhotoPath = viewModel.File.FilePath, oldFilePath = viewModel.File.FilePath;
                    viewModel.File.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);
                    viewModel.File.PhotoPath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedPicture.FileName);

                    var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.File.FilePath);
                    var uploadPhoto = await UploadToFileSystem(viewModel.AddedPicture, viewModel.File.PhotoPath);

                    if (uploadPhoto.Success && uploadFile.Success)
                    {
                        var result = await _fileService.Update(accessToken, viewModel.File);

                        if (!result.Success)
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, oldFilePath));
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, oldPhotoPath));
                        }
                    }
                    else
                    {
                        if (!uploadFile.Success)
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.File.FilePath));
                        }
                        if (!uploadPhoto.Success)
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
                        }
                        TempData["Message"] = "File isn't upload!";
                    }

                }
                TempData["Message"] = "Operation successfully";
            }
            catch (Exception exc)
            {
                // log exception here

                TempData["Message"] = "Operation unsuccessfully";
            }

            return RedirectToAction("ShowBooks");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(ResourceViewModel viewModel)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var deletedId = viewModel.DeletedBook.Id;

            var deleteFileFromFileSytem = await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath,viewModel.File.FilePath));
            var deletePhotoFromFileSytem = await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
            
            await _fileService.Delete(token, deletedId);

            TempData["Message"] = "Operation successfully";

            return RedirectToAction("ShowBooks");
        }
        #endregion

        #region FileUpload&Delete
        [HttpPost]
        public async Task<Result> UploadToFileSystem(IFormFile file, string uniqueFileName)
        {
            bool basePhotoPathExists = System.IO.Directory.Exists(DefaultPath.OriginalDefaultPhotoPath);
            if (!basePhotoPathExists) Directory.CreateDirectory(DefaultPath.OriginalDefaultPhotoPath);

            bool baseFilePathExists = System.IO.Directory.Exists(DefaultPath.OriginalDefaultFilePath);
            if (!baseFilePathExists) Directory.CreateDirectory(DefaultPath.OriginalDefaultFilePath);

            string filePath;

            if (file.FileName.Contains(".pdf"))
            {
                filePath = Path.Combine(DefaultPath.OriginalDefaultFilePath, uniqueFileName);
            }
            else
            {
                filePath = Path.Combine(DefaultPath.OriginalDefaultPhotoPath, uniqueFileName);
            }

            var extension = Path.GetExtension(file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return new SuccessResult();
            }
            TempData["Message"] = "File successfully uploaded to File System.";
            return new ErrorResult();
        }

        public async Task<Result> DeleteFileFromFileSystem(string fullFilePath)
        {
            if (System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Delete(fullFilePath);
            }
            TempData["Message"] = $"Removed this file successfully from File System.";
            return new SuccessResult();
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(File file)
        {
            string filePath, contentType, filename;

            filePath = Path.Combine(DefaultPath.OriginalDefaultFilePath, file.FilePath);
            contentType = "pdf/*";
            filename = file.FilePath;

            if (file == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, contentType, filename);
        }

        [HttpGet]
        public IActionResult Picture(string name)
        {
            try
            {
                string fullpath = Path.Combine(DefaultPath.OriginalDefaultPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(name);

                return File(content, "img/*");
            }
            catch
            {
                return Ok();
            }
        }

        /*public IActionResult QrCodeGenerator(string guid, string fileName)
        {
            //Generate normal QrCode
            //QRCodeWriter.CreateQrCode("Ehmed başına daş. Geberrr", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQR.png");

            string filename = Guid.NewGuid().ToString();

            *//*var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo(Defaults.DefaultUrlForQRCode+guid,
                "wwwroot/logo.png", 500).SaveAsPng(Defaults.DefaultPhotoPath + photopath);
            *//*
            return RedirectToAction("Index", "File");
        }*/
        #endregion

    }
}
