using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TransactionType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}
