namespace Library.Core.Extensions
{
    public static class UniqueNameGenerator
    {
        public static string UniqueFileNameGenerator(string name)
        {
            string hashCode = String.Format("{0:X}", name.GetHashCode());
            string fileName = Guid.NewGuid().ToString();
            return fileName + "-" + hashCode;
        }
        public static string UniqueFilePathGenerator(string fileName)
        {
            string hashCode = String.Format("{0:X}", fileName.GetHashCode());
            string filePath = Guid.NewGuid().ToString();
            return filePath + "-" + hashCode;
        }
        /*public void QrCodeGenerator(string guid, string qrCodePhotoPath)
        {
            var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo(SystemDefaults.DefaultUrlForQRCode + guid,
                "wwwroot/logo.png", 500).SaveAsPng(SystemDefaults.DefaultQRCodePhotoPath + qrCodePhotoPath);
        }*/
    }
}
