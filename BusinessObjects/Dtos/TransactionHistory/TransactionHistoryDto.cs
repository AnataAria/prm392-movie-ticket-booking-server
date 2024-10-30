using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.TransactionHistory
{
    public class TransactionHistoryDto
    {
        public string MovieName { get; set; } = string.Empty;
        public int? TicketQuantity { get; set; }
        public int? TotalPrice { get; set; }
        public string? Time { get; set; }
        public string Status { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
    }
}
