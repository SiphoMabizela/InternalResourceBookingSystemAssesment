using IRBSapi.Client.Interfaces;
using IRBSapi.Models;
using Microsoft.EntityFrameworkCore;

namespace IRBSapi.Client.Services
{
    public class BookingService : IBookings
    {
        Entities.IRBSEntities.IRBS_dataContext _context;

        public BookingService(Entities.IRBSEntities.IRBS_dataContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            try
            {
                var data = await _context.Bookings.Select(x => new Booking
                {
                    Id = x.Id,
                    ResourceId = x.ResourceId.Value,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    BookedBy = x.BookedBy,
                    Purpose = x.Purpose,
                }).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GetResource(int value)
        {
            var name = _context.Resources.Where(x => x.Id == value).Select(y => y.Name).FirstOrDefault();
            return name;
        }

        public async Task<bool> CreateBooking(Booking booking)
        {
            var bookingData = new Entities.IRBSEntities.Booking
            {
                ResourceId = booking.ResourceId,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                BookedBy = booking.BookedBy,
                Purpose = booking.Purpose
            };

            await _context.Bookings.AddAsync(bookingData);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Booking> GetBookingDetailsById(int id)
        {
            var bookingData = await _context.Bookings.Select(x => new Booking
            {
                Id = x.Id,
                ResourceId = x.ResourceId.Value,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                BookedBy = x.BookedBy,
                Purpose = x.Purpose
            }).Where(x => x.Id == id).FirstOrDefaultAsync();

            return bookingData;
        }

        public async Task<bool> UpdateBooking(Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(booking.Id);

            if (existingBooking == null)
            {
                return false;
            }

            bool isModified = false;

            if (existingBooking.ResourceId != booking.ResourceId)
            {
                existingBooking.ResourceId = booking.ResourceId;
                isModified = true;
            }

            if (existingBooking.StartTime != booking.StartTime)
            {
                existingBooking.StartTime = booking.StartTime;
                isModified = true;
            }

            if (existingBooking.EndTime != booking.EndTime)
            {
                existingBooking.EndTime = booking.EndTime;
                isModified = true;
            }

            if (existingBooking.BookedBy != booking.BookedBy)
            {
                existingBooking.BookedBy = booking.BookedBy;
                isModified = true;
            }

            if (existingBooking.Purpose != booking.Purpose)
            {
                existingBooking.Purpose = booking.Purpose;
                isModified = true;
            }

            if (isModified)
            {
                await _context.SaveChangesAsync();
            }

            return isModified;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);

            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}
