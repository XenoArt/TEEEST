using Microsoft.AspNetCore.Mvc;
using TEEEST.Models;
using TEEEST.Services;

namespace TEEEST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            try
            {
                if (booking.Duration == default && Request.Headers.TryGetValue("Duration", out var durationHeader))
                {
                    booking.Duration = TimeSpan.Parse(durationHeader!);
                }

                // Ensure UTC time
                booking.StartTimeUtc = DateTime.UtcNow;
                booking.UpdateEndedStatus();

                var result = await _bookingService.AddBookingAsync(booking);

                return Ok(new
                {
                    bookingType = result.BookingType,
                    price = result.Price,
                    date = result.Date,
                    start = result.Start,
                    end = result.End,
                    durationDisplay = result.DurationDisplay,
                    isEnded = result.IsEnded,
                    timeSummary = result.TimeSummary,
                    currentGeorgianTime = Booking.ToGeorgianTime(DateTime.UtcNow).ToString("HH:mm") // ✅ FIXED
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings.Select(b => new {
                b.BookingType,
                b.Price,
                b.Date,
                b.Start,
                b.End,
                b.DurationDisplay,
                b.IsEnded,
                b.TimeRemaining,
                b.TimeSummary,
                CurrentTime = Booking.ToGeorgianTime(DateTime.UtcNow).ToString("HH:mm:ss") // ✅ FIXED
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            return Ok(new
            {
                booking.BookingType,
                booking.Price,
                booking.Date,
                booking.Start,
                booking.End,
                booking.DurationDisplay,
                booking.IsEnded,
                booking.TimeRemaining,
                booking.TimeSummary,
                CurrentTime = Booking.ToGeorgianTime(DateTime.UtcNow).ToString("HH:mm:ss") // ✅ FIXED
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            var result = await _bookingService.EditBookingAsync(id, booking);
            if (result == null) return NotFound();

            return Ok(new
            {
                bookingType = result.BookingType,
                price = result.Price,
                date = result.Date,
                start = result.Start,
                end = result.End,
                durationDisplay = result.DurationDisplay,
                isEnded = result.IsEnded,
                timeSummary = result.TimeSummary,
                currentGeorgianTime = Booking.ToGeorgianTime(DateTime.UtcNow).ToString("HH:mm") // ✅ FIXED
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var success = await _bookingService.RemoveBookingAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
