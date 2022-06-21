using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageAirlineServices.Model
{
    public class SQLAirlineRepository : IAirlineRepository
    {
        private readonly AppDbContext context;

        public SQLAirlineRepository (AppDbContext context)
        {
            this.context = context;
        }

        public Airline Add(Airline airline)
        {
            context.Airlines.Add(airline);
            context.SaveChanges();
            return airline;
        }
        public Airline Delete(int id)
        {
            Airline airline = context.Airlines.Find(id);
            if (airline != null)
            {
                context.Airlines.Remove(airline);
                context.SaveChanges();
            }
            return airline;
        }
        public IEnumerable<Airline> GetAllAirlines()
        {
            return context.Airlines; 
        }

        public Airline GetAirline(int id)
        {
            //return context.Airlines.Find(id);
            return context.Airlines.FirstOrDefault(a => a.AirlineId == id);
        }

        public Airline UpdateAirline(Airline airchange)
        {
            var air = context.Airlines.Attach(airchange);
            air.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return airchange;
        }
    }
}
