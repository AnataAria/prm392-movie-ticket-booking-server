using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Event
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public int? TicketQuantity { get; set; }

    public string? Location { get; set; }

    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public string? Image { get; set; }

    public byte? Status { get; set; }

    public int? SponsorId { get; set; }

    public string? ServiceSponsor { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Account? Sponsor { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
