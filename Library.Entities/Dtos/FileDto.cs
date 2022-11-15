using Library.Entities.Abstract;

namespace Library.Entities.Dtos
{
    public class FileDto : BaseDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int OriginalLanguageId { get; set; }
        public int PublicationLanguageId { get; set; }
        public bool EditionStatus { get; set; }
        public bool ExistingStatus { get; set; }
        public int FileTypeId { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public string QrCodeFilePath { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PageNumber { get; set; }
        public string Description { get; set; }
        public string GUID { get; set; }
        public DateTime LastModified { get; set; }
    }
}
