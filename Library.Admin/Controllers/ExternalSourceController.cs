using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.DefaultSystemPath;
using Library.Core.Result.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Library.Core.Extensions;
using Library.Entities.Concrete;
using ZXing;
using Result = Library.Core.Result.Concrete.Result;

namespace Library.Admin.Controllers
{
    public class ExternalSourceController : Controller
    {
        private readonly IExternalSourceService _externalSource;

        public ExternalSourceController(IExternalSourceService externalSource)
        {
            _externalSource = externalSource;
        }

        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrWhiteSpace(HttpContext.Session.GetString("AdminAccessToken")))
                return RedirectToAction("Login", "Authentication");
            var model = new ExternalSourceViewModel();
            var result = await _externalSource.GetAll();
            model.SourceList = result.Data;
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            var result = await _externalSource.Get(token, id);
            var deleteResult = await _externalSource.Delete(token, id);
            if (deleteResult.Success)
                DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultExternalSourcePath, result.Data.PhotoPath));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SaveSource(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            if (id == 0)
                return View();
            var result = await _externalSource.Get(token, id);
            var model = new ExternalSourceViewModel
            {
                Source = result.Data
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSource(ExternalSourceViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            if (model.Source.Id == 0)
            {
                if (model.Photo != null)
                {
                    string fileName = UniqueNameGenerator.UniqueFileNameGenerator(model.Photo.Name);
                    var result = await UploadToFileSystem(model.Photo, fileName);
                    var extension = await _externalSource.Add(token, new ExternalSource
                    {
                        Name = model.Source.Name,
                        PhotoPath = fileName + result.Data,
                        SourceLink = model.Source.SourceLink,
                    });
                    if(!extension.Success)
                        DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultExternalSourcePath, model.Source.PhotoPath));
                    return RedirectToAction("Index");
                }

            }
            if (model.Source.Id != 0)
            {
                if (model.Photo != null)
                {
                    string fileName = UniqueNameGenerator.UniqueFileNameGenerator(model.Photo.FileName);
                    var result = await UploadToFileSystem(model.Photo, fileName);
                    if (result.Success)
                        DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultExternalSourcePath, model.Source.PhotoPath));
                    await _externalSource.Update(token, new ExternalSource
                    {
                        Id = model.Source.Id,
                        SourceLink = model.Source.SourceLink,
                        Name = model.Source.Name,
                        PhotoPath = fileName + result.Data
                    });
                    RedirectToAction("Index");
                }
                else
                {
                    await _externalSource.Update(token, new ExternalSource
                    {
                        Id = model.Source.Id,
                        SourceLink = model.Source.SourceLink,
                        Name = model.Source.Name,
                        PhotoPath = model.Source.PhotoPath
                    });
                }
            }

            return RedirectToAction("Index");
        }


        #region Upload and Delete Photo
        public IActionResult GetPhoto(string path)
        {
            try
            {
                string fullpath = Path.Combine(DefaultPath.OriginalDefaultExternalSourcePath, path);

                var content = System.IO.File.ReadAllBytes(fullpath);

                return File(content, "img/*");
            }
            catch
            {
                return Ok();
            }
        }
        private async Task<DataResult<string>> UploadToFileSystem(IFormFile file, string uniqueFileName)
        {
            bool basePhotoPathExists = Directory.Exists(DefaultPath.OriginalDefaultExternalSourcePath);
            if (!basePhotoPathExists) Directory.CreateDirectory(DefaultPath.OriginalDefaultExternalSourcePath);

            string extension = Path.GetExtension(file.FileName);
            StringBuilder builder = new StringBuilder();
            builder.Append(uniqueFileName);
            builder.Append(extension);
            string filePath = Path.Combine(DefaultPath.OriginalDefaultExternalSourcePath, builder.ToString());


            if (!System.IO.File.Exists(filePath))
            {
                using var stream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(stream);

                return new SuccessDataResult<string>(extension, "Successfully uploaded!");
            }
            return new ErrorDataResult<string>();
        }

        private Result DeleteFileFromFileSystem(string fullFilePath)
        {

            if (System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Delete(fullFilePath);
            }
            TempData["Message"] = $"Removed this file successfully from File System.";
            return new SuccessResult();
        }

        #endregion
    }
}
