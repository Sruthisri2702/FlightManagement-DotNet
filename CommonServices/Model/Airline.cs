using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CommonServices.Model
{
    public class Airline
    {
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string ContactNumber { get; set; }

        public string ContactAddress { get; set; }

        public string DisCode { get; set; }
        public double? DisAmount { get; set; }

        public bool? Blocked { get; set; }

        public string logo { get; set; }
        
        public virtual ICollection <Schedule> Schedules { get; set; }

    }
}
