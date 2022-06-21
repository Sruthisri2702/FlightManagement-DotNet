using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageBookingServices.Model
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly AppDbContext context;

        public SQLBookingRepository(AppDbContext context)
        {
            this.context = context;
        }

        public int Add(Booking booking)
        {
            Random rno = new Random();
            int randomnumber = rno.Next(1000000, 9999999); //7 digit PNR number
            booking.PNRNumber = randomnumber;
            context.Bookings.Add(booking);
            context.SaveChanges();
            return randomnumber;
        }

        public Booking Delete(int id)
        {
            Booking book = context.Bookings.FirstOrDefault(x=>x.BookingId == id);
            if (book != null)
            {
                context.Bookings.Remove(book);
                context.SaveChanges();
            }
            return book;
        }

        public IQueryable<Ticket> GetBookingsByEmail(string email)
        {
            //return context.Bookings.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).ToList();
            var res = (from book in context.Bookings.AsQueryable()
                       join sch in context.Schedule.AsQueryable()
                        on book.ScheduleId equals sch.ScheduleId
                       where book.Email.ToLower().Trim() == email.ToLower().Trim()
                       select new Ticket
                       {
                           Name = book.Name,
                           Email = book.Email,
                           FlightNumber = sch.FlightNumber,
                           FromPlace = sch.FromPlace,
                           ToPlace = sch.ToPlace,
                           TicketCost = sch.TotalCost,
                           StartDateTime = sch.StartDateTime,
                           EndDateTime = sch.EndDateTime,
                           NoOfSeats = book.NoOfSeats,
                           BookingId = book.BookingId

                       }
                      ); ;
            return res;

        }

        public IQueryable<Ticket> GetBookingByPNR(string pnr)
        {
            //Booking book = context.Bookings.FirstOrDefault(x => x.PNRNumber.ToString() == pnr);
            //return book;
            var res = (from book in context.Bookings.AsQueryable()
                       join sch in context.Schedule.AsQueryable()
                       on book.ScheduleId equals sch.ScheduleId
                       where book.PNRNumber.ToString() == pnr
                       select new Ticket
                       {
                           Name = book.Name,
                           Email = book.Email,
                           FlightNumber = sch.FlightNumber,
                           FromPlace = sch.FromPlace,
                           ToPlace = sch.ToPlace,
                           TicketCost = sch.TotalCost,
                           StartDateTime = sch.StartDateTime,
                           EndDateTime = sch.EndDateTime,
                           NoOfSeats = book.NoOfSeats,
                           BookingId = book.BookingId

                       }
                       );
            return res;   

        }
        public IEnumerable<Booking> GetAllBookings()
        {
            return context.Bookings.ToList();
        }


    }
}
