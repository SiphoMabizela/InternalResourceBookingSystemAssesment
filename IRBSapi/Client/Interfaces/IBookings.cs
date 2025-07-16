using IRBSapi.Models;

namespace IRBSapi.Client.Interfaces
{
    public interface IBookings
    {
        public Task<List<Booking>> GetAllBookings();
        public Task<bool> CreateBooking(Booking booking);
        public Task<Booking> GetBookingDetailsById(int id);
        public Task<bool> UpdateBooking(Booking booking);
        public Task<bool> DeleteBooking(int id);
    }
}
