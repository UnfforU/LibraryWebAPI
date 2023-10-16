namespace LibraryWebAPI.Models.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartDateTime { get; set; } 
        public DateTime EndDateTime { get; set; }

    }
}
