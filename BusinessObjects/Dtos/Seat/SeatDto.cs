using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Seat
{
    public class SeatDto
    {
        [JsonPropertyName("seatId")]
        public int Id { get; set; }

        [JsonPropertyName("seatNumber")]
        public string? SeatNumber { get; set; }

        [JsonPropertyName("cinemaName")]
        public string? CinemaRoomName { get; set; }
    }
}
