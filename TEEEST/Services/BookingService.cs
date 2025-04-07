using Microsoft.EntityFrameworkCore;
using TEEEST.Data;
using TEEEST.Models;

namespace TEEEST.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            if (string.IsNullOrWhiteSpace(booking.BookingType))
                throw new ArgumentException("Booking type is required");

            if (booking.Price <= 0)
                throw new ArgumentException("Price must be positive");

            if (booking.Duration <= TimeSpan.Zero)
                throw new ArgumentException("Duration must be positive");

            // Ensure UTC time
            booking.StartTimeUtc = booking.StartTimeUtc.Kind == DateTimeKind.Utc
                ? booking.StartTimeUtc
                : booking.StartTimeUtc.ToUniversalTime();

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> RemoveBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Booking?> EditBookingAsync(int id, Booking updatedBooking)
        {
            var existing = await _context.Bookings.FindAsync(id);
            if (existing == null) return null;

            existing.BookingType = updatedBooking.BookingType;
            existing.Price = updatedBooking.Price;
            existing.Duration = updatedBooking.Duration;
            existing.StartTimeUtc = updatedBooking.StartTimeUtc;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .OrderBy(b => b.StartTimeUtc)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }
    }
}
