namespace Library.WebUI.Services.Abstract
{
    public interface IFileService
    {
        //string SecureSaveFile(byte[] content, string extension, string path);
        public string GetFileBase64(string name, string path);
        public byte[] GetFile(string name, string path);
    }
}
