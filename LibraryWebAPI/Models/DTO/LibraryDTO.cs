namespace LibraryWebAPI.Models.DTO
{
    public class LibraryDTO
    {
        public Guid LibraryId { get; set; }
        public string Name { get; set; } = null!;
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();

    }
}
