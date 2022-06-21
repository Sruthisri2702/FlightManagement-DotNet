using CommonServices.Model;
//using ManageAirlineServices.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommonServices.Model
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string FlightNumber { get; set; }

        [ForeignKey("AirlineID")]
        public int AirlineId { get; set; }

        public virtual Airline Airline { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }


        public double TotalCost { get; set; }

        //rows are seats coming from the ticket class once a booking is done
        public int Rows { get; set; }

        public string Meal { get; set; }

        public string ScheduledDays { get; set; }

        public string InstrumentUsed { get; set; }
        public int BusinessSeats { get; set; }

        public int NonBusinessSeats { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
