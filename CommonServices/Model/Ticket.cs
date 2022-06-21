using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonServices.Model
{
    public class Ticket
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string FlightNumber { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public double TicketCost { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public int NoOfSeats { get; set; }

        public int BookingId { get; set; }



    }
}
