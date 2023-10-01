using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Models;

public partial class Book
{
    public Guid BookId { get; set; }

    public string Name { get; set; } = null!;

    public Guid AuthorId { get; set; }

    public string? Description { get; set; }

    public Guid LibraryId { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsBooked { get; set; }

    public Guid? OwnerId { get; set; }

    public DateTime? BookedDate { get; set; }

    public virtual Author AuthorNavigation { get; set; } = null!;

    public virtual Library LibraryNavigation { get; set; } = null!;

    public virtual User? OwnerNavigation { get; set; }
}
