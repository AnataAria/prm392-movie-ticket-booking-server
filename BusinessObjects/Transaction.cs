using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Transaction
{
    public int Id { get; set; }

    public int? MovieID { get; set; }

    public int? SolvedTicketId { get; set; }

    public int? TypeId { get; set; }

    public string? Status { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual SolvedTicket? SolvedTicket { get; set; }

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = [];

    public virtual TransactionType? Type { get; set; }
}
