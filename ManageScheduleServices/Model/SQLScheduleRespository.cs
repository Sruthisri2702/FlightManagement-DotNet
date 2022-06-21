using CommonServices.Model;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageScheduleServices.Model
{
    public class SQLScheduleRespository : IScheduleRepository,IConsumer<RabbitMQData>
    {
        private readonly AppDbContext context;

        public SQLScheduleRespository(AppDbContext context)
        {
            this.context = context;
        }
        public Schedule Add(Schedule sch)
        {
            context.Schedule.Add(sch);
            context.SaveChanges();
            return sch;
        }

        public async Task Consume(ConsumeContext<RabbitMQData> _mqcontext)
        {
            int ScheduleId = _mqcontext.Message.ScheduleId;
            var inventory = context.Schedule.Find(ScheduleId);
            inventory.Rows = inventory.Rows - _mqcontext.Message.Rows;
            inventory.BusinessSeats = inventory.BusinessSeats - _mqcontext.Message.BusinessSeats;
            inventory.NonBusinessSeats = inventory.NonBusinessSeats - _mqcontext.Message.NonBusinessSeats;
            context.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public Schedule Delete(int id)
        {
            Schedule sch = context.Schedule.Find(id);
            if (sch != null)
            {
                context.Schedule.Remove(sch);
                context.SaveChanges();
            }
            return sch;
        }
        public IQueryable GetAllSchedules()
        {
            try
            {
                //var Li = context.Schedule.ToList();
                var Li = (from sch in context.Schedule.AsQueryable()
                          join air in context.Airlines.AsQueryable()
                          on sch.AirlineId equals air.AirlineId
                          select new
                          {
                              sch.ScheduleId,
                              sch.FlightNumber,
                              sch.StartDateTime,
                              sch.EndDateTime,
                              sch.FromPlace,
                              sch.ToPlace,
                              sch.BusinessSeats,
                              sch.NonBusinessSeats,
                              sch.TotalCost,
                              sch.Meal,
                              sch.ScheduledDays,
                              sch.InstrumentUsed,
                              sch.Rows,
                              air.AirlineName                             

                          });
                return Li;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public IQueryable GetScheduleBasedOnPlaces(string fromplace, string toplace)
        {
            //var res = context.Schedule.Where(x => x.FromPlace.ToLower() == from.ToLower() && x.ToPlace.ToLower() == to.ToLower()).ToList();
            //return res;
            //airline name to be fetched
            try
            {
                var res = (from flight in context.Schedule.Where(x => x.FromPlace.ToLower() == fromplace.ToLower()
                           && x.ToPlace.ToLower() == toplace.ToLower()).AsQueryable()
                           join airline in context.Airlines.AsQueryable()
                           on flight.AirlineId equals airline.AirlineId
                           select new
                           {
                               flight.ScheduleId,
                               flight.FlightNumber,
                               flight.StartDateTime,
                               flight.EndDateTime,
                               flight.FromPlace,
                               flight.ToPlace,
                               flight.BusinessSeats,
                               flight.NonBusinessSeats,
                               flight.TotalCost,
                               flight.Meal,
                               flight.ScheduledDays,
                               flight.InstrumentUsed,
                               flight.Rows,
                               airline.AirlineName
                           });

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Schedule UpdateSchedule(Schedule schchange)
        {
            var sch = context.Schedule.Attach(schchange);
            sch.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return schchange;
        }
    }
}
