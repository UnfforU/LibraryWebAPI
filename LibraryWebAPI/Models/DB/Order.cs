namespace LibraryWebAPI.Models.DB
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
