using TEEEST.Models;

namespace TEEEST.Services
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(Booking booking);
        Task<bool> RemoveBookingAsync(int id);
        Task<Booking?> EditBookingAsync(int id, Booking updatedBooking);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);

    }
}
