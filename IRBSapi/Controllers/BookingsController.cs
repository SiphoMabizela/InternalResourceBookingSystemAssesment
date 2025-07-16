using IRBSapi.Client.Interfaces;
using IRBSapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRBSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookings _iBookings;

        public BookingsController(IBookings iBookings)
        {
            _iBookings = iBookings;
        }

        [HttpGet]
        public async Task<List<Booking>> GetBookingsAsync() => await _iBookings.GetAllBookings();

        [HttpPost]
        public async Task<bool> CreateBooking(Booking booking) => await _iBookings.CreateBooking(booking);

        [HttpGet("{id}")]
        public async Task<Booking> GetBookingById(int id) => await _iBookings.GetBookingDetailsById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.Id)
                return BadRequest();

            var success = await _iBookings.UpdateBooking(booking);

            if (success)
                return Ok();

            return StatusCode(500, "Failed to update");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var deleted = await _iBookings.DeleteBooking(id);
            if (deleted)
                return Ok();
            return StatusCode(500, "Delete failed");
        }
    }
}
