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
        public IActionResult File(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultFilePath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "pdf/*");
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
    }
}
