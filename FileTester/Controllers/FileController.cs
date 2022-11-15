using FileTester.Models;
using FileTester.MySystem;
using FileTester.Services.Abstract;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using File = Library.Entities.Concrete.File;

namespace FileTester.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        private async Task<FileUploadViewModel> LoadAllFiles()
        {
            var viewModel = new FileUploadViewModel();
            var result = await _fileService.GetAll();
            viewModel.Files = result.Data;
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
                bool basePathExists = System.IO.Directory.Exists(Defaults.DefaultPhotoPath);
                if (!basePathExists) Directory.CreateDirectory(Defaults.DefaultPhotoPath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);

                string filePath;

                if (file.FileName.Contains(".pdf"))
                {
                    filePath = Path.Combine(Defaults.DefaultFilePath, file.FileName);
                }
                else
                {
                    filePath = Path.Combine(Defaults.DefaultPhotoPath, file.FileName);
                }

                var extension = Path.GetExtension(file.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var mustBeAddedFile = new File();

                    await _fileService.Add(mustBeAddedFile);
                }
            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {
            var result = await _fileService.GetAll();
            var file = result.Data.Where(x => x.Id == id).FirstOrDefault();

            string filePath, contentType, filename;

            filePath = Path.Combine(Defaults.DefaultFilePath, file.FilePath);
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

        public async Task<IActionResult> DeleteFileFromFileSystem(int id)
        {

            var result = await _fileService.GetAll();
            var file = result.Data.Where(x => x.Id == id).FirstOrDefault();

            string filePath, photoPath;


            filePath = Path.Combine(Defaults.DefaultFilePath, file.FilePath);
            photoPath = Path.Combine(Defaults.DefaultPhotoPath, file.PhotoPath);

            StringBuilder fileName = new StringBuilder(file.PhotoPath);
            fileName.Append(" and ");
            fileName.Append(file.FilePath);

            if (file == null) return null;

            if (System.IO.File.Exists(photoPath))
            {
                System.IO.File.Delete(photoPath);
            }
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await _fileService.Delete(file.Id);
            TempData["Message"] = $"Removed {fileName} successfully from File System.";
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

        public IActionResult QrCodeGenerator(string guid,string fileName)
        {
            //Generate normal QrCode
            //QRCodeWriter.CreateQrCode("Ehmed başına daş. Geberrr", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQR.png");

            string filename = Guid.NewGuid().ToString();

            var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo(Defaults.DefaultUrlForQRCode+guid,
                "wwwroot/logo.png", 500).SaveAsPng(Defaults.DefaultPhotoPath + photopath);

            return RedirectToAction("Index", "File");
        }
    }
}