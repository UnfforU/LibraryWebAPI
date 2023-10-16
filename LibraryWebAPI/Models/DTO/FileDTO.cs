using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Models.DTO
{
    public class FileDTO
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = "";
        public string FileContent { get; set; } = "";
        public Guid? BookId { get; set; }
    }
}
