namespace LibraryWebAPI.Models.DB
{
    public class File
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; } = null!;
        public Guid? BookId { get; set; }

        public virtual Book? Book { get; set; }

    }
}
