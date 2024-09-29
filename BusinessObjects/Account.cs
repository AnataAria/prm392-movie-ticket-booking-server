using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Account
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public int? RoleId { get; set; }

    public byte? Status { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public double? Wallet { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<SolvedTicket> SolvedTickets { get; set; } = new List<SolvedTicket>();
}
