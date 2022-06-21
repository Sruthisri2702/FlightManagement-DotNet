using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageAirlineServices.Model
{
    public interface IAirlineRepository
    {
        Airline Add(Airline airline);
        Airline Delete(int id);
        IEnumerable<Airline> GetAllAirlines();

        Airline GetAirline(int id);
        Airline UpdateAirline(Airline airchange);
    }
}
