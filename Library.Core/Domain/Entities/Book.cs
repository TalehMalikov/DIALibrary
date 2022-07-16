namespace Library.Core.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int OriginalLangaugeId { get; set; }
        public DateTime LastModified{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
