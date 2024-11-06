namespace WebApplication_DatabaseFirst_Demo.Repositories
{
    public interface IBookingServices
    {
        List<Booking> GetAllBookings();
        int AddNewBooking(Booking booking);
        Booking GetBookingById(int id);
        string UpdateBooking(Booking booking);
        string DeleteBooking(int id);
    }
}
