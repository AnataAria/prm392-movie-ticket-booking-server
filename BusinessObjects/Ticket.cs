using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Ticket
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public int? Price { get; set; }

    public byte? Status { get; set; }

    public int? Quantity { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<SolvedTicket> SolvedTickets { get; set; } = new List<SolvedTicket>();
}
