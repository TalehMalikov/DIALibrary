using Library.Entities.Dtos;
using Library.WebUI.Services.Abstract;
using Library.WebUI.Systems;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Library.WebUI.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IFileService _fileService;

        public ResourcesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult Picture(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "img/*");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        [HttpGet]
        public IActionResult OpenFile(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultFilePath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "application/pdf");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {
            var result = await _fileService.GetFileById(id);
            var file = result.Data;

            string filePath, contentType, filename;

            filePath = Path.Combine(SystemDefaults.DefaultFilePath, file.FilePath);
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
        #region In order to adding Admin Panel
        // These Methods must be updated when try to use in Admin Panel
        public async Task<IActionResult> DeleteFileFromFileSystem(int id)
        {

            var file = await _fileService.GetFileById(id);

            string filePath, photoPath;


            filePath = Path.Combine(SystemDefaults.DefaultFilePath, file.Data.FilePath);
            photoPath = Path.Combine(SystemDefaults.DefaultPhotoPath, file.Data.PhotoPath);

            StringBuilder fileName = new StringBuilder(file.Data.PhotoPath);
            fileName.Append(" and ");
            fileName.Append(file.Data.FilePath);

            if (file == null) return null;

            if (System.IO.File.Exists(photoPath))
            {
                System.IO.File.Delete(photoPath);
            }
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            //await _fileService.Delete(file.Id);
            TempData["Message"] = $"Removed {fileName} successfully from File System.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, FileAuthorDto addedFile)
        {
            foreach (var file in files)
            {
                if (!Directory.Exists(SystemDefaults.DefaultPhotoPath)) Directory.CreateDirectory(SystemDefaults.DefaultPhotoPath);

                if (!Directory.Exists(SystemDefaults.DefaultFilePath)) Directory.CreateDirectory(SystemDefaults.DefaultFilePath);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string filePath;

                if (file.FileName.Contains(".pdf"))
                {
                    filePath = Path.Combine(SystemDefaults.DefaultFilePath, file.FileName);
                }
                else
                {
                    filePath = Path.Combine(SystemDefaults.DefaultPhotoPath, file.FileName);
                }

                var extension = Path.GetExtension(file.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var mustBeAddedFile = new FileAuthorDto();

                    //await _fileService.Add(mustBeAddedFile.File);
                }
            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        #endregion
    }
}
