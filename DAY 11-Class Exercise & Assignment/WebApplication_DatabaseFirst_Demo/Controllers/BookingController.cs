using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_DatabaseFirst_Demo.Repositories;


namespace WebApplication_DatabaseFirst_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingServices _bookingServices;

        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        // GET: api/Booking
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            try
            {
                var bookings = _bookingServices.GetAllBookings();
                if (bookings != null && bookings.Count > 0)
                    return Ok(bookings);  // Return status 200 with the list of bookings
                return NoContent();  // Return status 204 if no bookings found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Booking/{id}
        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            try
            {
                var booking = _bookingServices.GetBookingById(id);
                if (booking != null)
                    return Ok(booking);  // Return status 200 with the booking data
                return NotFound();  // Return status 404 if booking not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Booking
        [HttpPost]
        public IActionResult AddNewBooking([FromBody] Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest("Booking data is null.");
                }

                int bookingId = _bookingServices.AddNewBooking(booking);
                if (bookingId > 0)
                    return CreatedAtAction(nameof(GetBookingById), new { id = bookingId }, booking); // Return status 201 (Created)
                return BadRequest("Failed to create booking.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Booking/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest("Booking data is null.");
                }

                if (id != booking.BookingId)
                {
                    return BadRequest("Booking ID mismatch.");
                }

                string updateResult = _bookingServices.UpdateBooking(booking);
                if (updateResult == "Booking record updated successfully")
                    return Ok(updateResult);  // Return status 200 with success message
                return NotFound("Booking not found.");  // Return status 404 if booking is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Booking/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                string deleteResult = _bookingServices.DeleteBooking(id);
                if (deleteResult.Contains("Removed"))
                    return Ok(deleteResult);  // Return status 200 with success message
                return NotFound(deleteResult);  // Return status 404 if booking is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
