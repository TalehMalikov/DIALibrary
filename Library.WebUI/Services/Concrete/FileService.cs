using Library.Core.Utils;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class FileService : IFileService
    {
       /* public string SecureSaveFile(byte[] content, string extension, string path)
        {
            Directory.CreateDirectory(path);

            using var stream = new MemoryStream(content);

            var hash = SecurityUtil.ComputeSha256Hash(stream);

            string fileName = hash + $".{extension}";
            string filePath = Path.Combine(path, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            stream.WriteTo(fileStream);

            return fileName; 
        }
*/
        public string GetFileBase64(string name, string path)
        {
            if (name is null || path is null)
            {
                return null;
            }

            return Convert.ToBase64String(GetFile(name, path));
        }

        public byte[] GetFile(string name, string path)
        {
            if (name is null || path is null)
            {
                return null;
            }

            return File.ReadAllBytes(Path.Combine(path, name));
        }
    }
}
