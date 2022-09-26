namespace Library.Core.DefaultSystemPath
{
    public class DefaultPath
    {
        #region Original
        public const string OriginalDefaultPhotoPath = @"C:\DIALibrary\BookPhotos\";
        public const string OriginalDefaultFilePath = @"C:\DIALibrary\BookFiles\";
        public const string OriginalDefaultUrlForQRCode = "https://localhost:7058/File/ShowFileInfo?guid=";
        public const string OriginalDefaultQRCodePhotoPath = @"C:\DIALibrary\QRCodeImages\";
        #endregion

        public const string DefaultPhotoPath = "wwwroot/images/FakeBookPhotos/";
        public const string DefaultFilePath = "wwwroot/images/FakeBookFiles/";
        //public const string DefaultUrlForQRCode = "https://localhost:7058/File/ShowFileInfo?guid=";
        //public const string DefaultQRCodePhotoPath = @"C:\DIALibrary\QRCodeImages\";
    }
}
