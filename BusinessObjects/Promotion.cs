using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Promotion
{
    public int Id { get; set; }

    public int? Condition { get; set; }

    public double? Discount { get; set; }

    public virtual ICollection<SolvedTicket> SolvedTickets { get; set; } = new List<SolvedTicket>();
}
