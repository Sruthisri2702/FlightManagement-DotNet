using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageBookingServices.Model
{
    public interface IBookingRepository
    {
        int Add(Booking booking);
        Booking Delete(int id);

        //IEnumerable<Booking> GetBookingsByEmail(string email);

        //Booking GetBookingByPNR(string pnr);

        IEnumerable<Booking> GetAllBookings();
        IQueryable<Ticket> GetBookingByPNR(string pnr);
        IQueryable<Ticket> GetBookingsByEmail(string email);


    }
}
