using IRBSapi.Client.Interfaces;
using IRBSapi.Models;
using Microsoft.EntityFrameworkCore;

namespace IRBSapi.Client.Services
{
    public class ResourceBookingService : IResourceBooking
    {
        Entities.IRBSEntities.IRBS_dataContext _context;

        public ResourceBookingService(Entities.IRBSEntities.IRBS_dataContext context)
        {
            _context = context;
        }

        public async Task<List<ResourceBooking>> GetAllResourceBooking()
        {
            try
            {
                var data = await _context.Resources.Select(x => new ResourceBooking
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsAvailable = x.IsAvailable,
                    IsUnderMaintenance = x.IsUnderMaintenance
                }).Where(x => x.IsUnderMaintenance == false && x.IsAvailable == true).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
