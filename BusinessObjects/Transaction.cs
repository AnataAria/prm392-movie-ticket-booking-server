using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Transaction
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public int? SolvedTicketId { get; set; }

    public int? TypeId { get; set; }

    public string? Status { get; set; }

    public virtual Event? Event { get; set; }

    public virtual SolvedTicket? SolvedTicket { get; set; }

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();

    public virtual TransactionType? Type { get; set; }
}
