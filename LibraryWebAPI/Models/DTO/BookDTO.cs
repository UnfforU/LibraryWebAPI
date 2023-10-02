namespace LibraryWebAPI.Models.DTO
{
    public class BookDTO
    {
        public Guid BookId { get; set; }
        public string Name { get; set; } = "";
        public string AuthorName { get; set; } = "";
        public string? Description { get; set; }
        public Guid LibraryId { get; set; }
        public bool? IsBooked { get; set; } = false;
        public Guid? OwnerId { get; set; } = Guid.Empty;
        public DateTime? BookedDate { get; set; }
    }
}
