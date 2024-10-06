using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = [];
}
