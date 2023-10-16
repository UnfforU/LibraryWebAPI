namespace LibraryWebAPI.Models.DTO
{
    public class BookDTO
    {
        public Guid BookId { get; set; }
        public string Name { get; set; } = null!;
        public List<AuthorDTO> Authors { get; set;} = new();
        public string? Description { get; set; }
        public byte[]? Cover { get; set; }
        public Guid LibraryId { get; set; }

        public List<OrderDTO> Orders { get; set; } = new();

    }
}
