using CommonServices.Model;
using ManageBookingServices.Model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageBookingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize(Roles = "user")]
    public class BookingController : ControllerBase
    {
        private IBookingRepository book;
        private readonly IBus _bus;
        public BookingController (IBookingRepository _book, IBus bus)
        {
            book = _book;
            _bus = bus;
        }
        
        
        [HttpGet]
        [Route("GET")]
        public string get()
        {
            return "Hello world!";
        }

        [HttpPost]
        [Route("Add")]
        public async Task<int> AddBooking([FromBody]Booking bookings)
        {
            int pnr= book.Add(bookings);
            int BusinessClassSeats = bookings.PassengerDetails.Where(x => x.SeatType.ToLower() == "business").Count();
            int NonBusinessClassSeats = bookings.PassengerDetails.Where(x => x.SeatType.ToLower() == "nonbusiness").Count();
            Uri uri = new Uri("rabbitmq://localhost/InventoryCountQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new RabbitMQData
            {
                BusinessSeats = BusinessClassSeats,
                NonBusinessSeats = NonBusinessClassSeats,
                ScheduleId = bookings.ScheduleId,
                Rows = bookings.NoOfSeats
            });
            return pnr;
        }



        [HttpDelete]
        [Route("Delete")]
        public Booking CancelBooking(int id)
        {
            return book.Delete(id);
        }

        [HttpGet]
        [Route("GetByEmail")]
        public IQueryable<Ticket> GetBookingsByEmail(string email)
        {
            return book.GetBookingsByEmail(email);
        }

        [HttpGet]
        [Route("GetByPNR")]
        public IQueryable<Ticket> GetBookingByPNR(string pnr)
        {
            return book.GetBookingByPNR(pnr);
        }
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Booking> GetAllBookings()
        {
            return book.GetAllBookings();
        }


    }
}
