using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonServices.Model
{
    public class RabbitMQData
    {
        public int BusinessSeats { get; set; }
        public int NonBusinessSeats { get; set; }

        //rows are seats
        public int Rows { get; set; }

        public int ScheduleId { get; set; }

    }
}
