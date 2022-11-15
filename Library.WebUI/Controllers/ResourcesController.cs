using Library.Entities.Dtos;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Library.Core.DefaultSystemPath;

namespace Library.WebUI.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IFileService _fileService;

        private string ReformatString(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (c == '/' | c == '\\')
                    sb.Append("");
                else sb.Append(c);
            }
            return sb.ToString();
        }
        public ResourcesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult Picture(string name)
        {
            try
            {
                name = ReformatString(name);
                string fullPath = Path.Combine(DefaultPath.OriginalDefaultPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "img/*");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        [HttpGet]
        public IActionResult QrCodePhoto(string name)
        {
            try
            {
                name = ReformatString(name);
                string fullPath = Path.Combine(DefaultPath.OriginalDefaultQRCodePhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "img/*");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }
        
        public IActionResult ActivityPhoto(string name)
        {
            try
            {
                name = ReformatString(name);
                string fullPath = Path.Combine(DefaultPath.OriginalDefaultActivityPhotoPath, name);

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
                name = ReformatString(name);
                string fullPath = Path.Combine(DefaultPath.OriginalDefaultFilePath, name);

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

            filePath = Path.Combine(DefaultPath.OriginalDefaultFilePath, file.FilePath);

            contentType = $"{Path.GetExtension(file.FilePath).TrimStart('.')}/*";
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
    }
}
