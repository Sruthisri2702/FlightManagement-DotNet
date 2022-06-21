using CommonServices.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ManageBookingServices.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Schedule> Schedule { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<Booking>().HasOne<Schedule>(e => e.Schedule).WithMany(e => e.Bookings).HasForeignKey(e => e.ScheduleId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            
            model.Entity<Booking>().HasMany<PassengerDetails>(e => e.PassengerDetails).WithOne(e => e.Booking).HasForeignKey(e => e.BookingId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            
            model.Entity<Booking>().HasOne<User>(e => e.User).WithMany(e => e.Bookings).HasForeignKey(e => e.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
