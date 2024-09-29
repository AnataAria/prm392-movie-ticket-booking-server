using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TransactionHistory
{
    public int Id { get; set; }

    public int? TransactionId { get; set; }

    public int? Price { get; set; }

    public DateOnly? Time { get; set; }

    public string? Status { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
