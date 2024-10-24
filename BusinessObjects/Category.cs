using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Category
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = [];
}
