using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class SolvedTicket
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? TicketId { get; set; }

    public int? Quantity { get; set; }

    public int? TotalPrice { get; set; }

    public int? PromotionId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Promotion? Promotion { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
