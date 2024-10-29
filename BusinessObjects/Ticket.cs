using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Ticket
{
    public int Id { get; set; }

    public int? MovieID { get; set; }
    public int SeatID { get; set; }
    public int ShowtimeID { get; set; }

    public int? Price { get; set; }

    public byte? Status { get; set; }

    public int? Quantity { get; set; }

    public virtual Movie? Movie { get; set; }
    public virtual Seat? Seat { get; set; }
    public virtual ShowTime? Showtime { get; set; }


    public virtual ICollection<SolvedTicket> SolvedTickets { get; set; } = [];
}
