using IRBSapi.Client.Interfaces;
using IRBSapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IRBSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceBookingController : ControllerBase
    {
        private readonly IResourceBooking _iResourceBooking;

        public ResourceBookingController(IResourceBooking iResourceBooking)
        {
            _iResourceBooking = iResourceBooking;
        }

        [HttpGet]
        public async Task<List<ResourceBooking>> GetResourceBookingAsync() => await _iResourceBooking.GetAllResourceBooking();
    }
}
