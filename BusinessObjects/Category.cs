using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Category
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
