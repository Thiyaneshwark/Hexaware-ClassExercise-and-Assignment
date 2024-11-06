using Microsoft.EntityFrameworkCore;
using WebApplication_DatabaseFirst_Demo.Models;
using WebApplication_DatabaseFirst_Demo.Repositories;

namespace WebApplication_DatabaseFirst_Demo.Repositories
{
    public class BookingServices : IBookingServices
    {
        private BikeStoresContext _context;
        public BookingServices(BikeStoresContext dbContext)
        {
            _context = dbContext;
        }
        public int AddNewBooking(Booking booking)
        {
            try
            {
                if (booking != null)
                {
                    _context.Bookings.Add(booking);
                    _context.SaveChanges();
                    return booking.BookingId;
                }
                else return 0;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public string DeleteBooking(int id)
        {
            if (id != null)
            {
                var booking = _context.Bookings.FirstOrDefault(x => x.BookingId == id);
                if (booking != null)
                {
                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();
                    return "the given Department id " + id + "Removed";
                }
                else
                    return "Something went wrong with deletion";

            }
            return "Id should not be null or zero";
        }

        public List<Booking> GetAllBookings()
        {
            var bookings = _context.Bookings.ToList();
            if (bookings.Count > 0)
                return bookings;
            return null;
        }

        public Booking GetBookingById(int id)
        {
            if (id != null || id != 0)
            {
                var booking = _context.Bookings.FirstOrDefault(x => x.BookingId == id);
                if (booking != null)
                    return booking;
                else
                    return null;
            }
            return null;
        }

        public string UpdateBooking(Booking booking)
        {
            var existingBooking = _context.Bookings.FirstOrDefault(x => x.BookingId == booking.BookingId);
            if (existingBooking != null)
            {
                // Update the existing booking details
                existingBooking.RoomId = booking.RoomId;
                existingBooking.CustomerName = booking.CustomerName;
                existingBooking.CheckInDate = booking.CheckInDate;
                existingBooking.CheckOutDate = booking.CheckOutDate;

                // Mark the entity as modified and save changes
                _context.Entry(existingBooking).State = EntityState.Modified;
                _context.SaveChanges();

                return "Booking record updated successfully";
            }

            return "Booking record not found";
        }

    }
}
