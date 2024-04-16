using braidingweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace braidingweb.Controllers
{
    // Example of a new controller specifically for handling bookings
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly BookingsConfirmController.IBookingManager _bookingManager;

        public BookingController(BookingsConfirmController.IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        [HttpPost("/submit-booking")]
        public async Task<IActionResult> SubmitBooking([FromBody] Booking booking)
        {
            // Process the booking (save to database, send emails, etc.)
            bool success = await _bookingManager.ProcessBookingAsync(booking);

            if (success)
            {
                return Ok(new { message = "Booking submitted successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to submit booking" });
            }
        }
    }

}
