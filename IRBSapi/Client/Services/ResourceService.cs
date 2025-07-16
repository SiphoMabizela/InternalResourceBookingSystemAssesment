using IRBSapi.Client.Interfaces;
using IRBSapi.Models;
using Microsoft.EntityFrameworkCore;

namespace IRBSapi.Client.Services
{
    public class ResourceService : IResources
    {
        Entities.IRBSEntities.IRBS_dataContext _context;

        public ResourceService(Entities.IRBSEntities.IRBS_dataContext context)
        {
            _context = context;
        }
        public async Task<List<Resource>> GetAllResources()
        {
            try
            {
                var data = await _context.Resources.Select(x => new Resource
                {
                    Id = x.Id,
                    Name = x.Name,
                    Capacity = x.Capacity,
                    Description = x.Description,
                    IsAvailable = x.IsAvailable,
                    IsUnderMaintenance = x.IsUnderMaintenance,
                    Location = x.Location
                     ,
                    LinkedBookings = _context.Bookings
                    .Where(b => b.ResourceId == x.Id)
                    .Select(b => new Booking
                    {
                        Id = b.Id,
                        ResourceId = b.ResourceId.Value,
                        StartTime = b.StartTime,
                        EndTime = b.EndTime,
                        BookedBy = b.BookedBy,
                        Purpose = b.Purpose
                    }).ToList()
                }).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateResource(Resource resource)
        {
            var resourceData = new Entities.IRBSEntities.Resource
            {
                Name = resource.Name,
                Capacity = resource.Capacity,
                Description = resource.Description,
                IsAvailable = resource.IsAvailable,
                IsUnderMaintenance = resource.IsUnderMaintenance,
                Location = resource.Location
            };

            await _context.Resources.AddAsync(resourceData);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Resource> GetResourceDetailsById(int id)
        {
            var resourceData = await _context.Resources.Select(x => new Resource{
                Id = x.Id,
                Name = x.Name,
                Capacity = x.Capacity,
                Description = x.Description,
                IsAvailable= x.IsAvailable,
                IsUnderMaintenance = x.IsUnderMaintenance,
                Location = x.Location
            }).Where(x => x.Id == id).FirstOrDefaultAsync();

            return resourceData;
        }

        public async Task<bool> UpdateResource(Resource resource)
        {
            var existingResource = await _context.Resources.FindAsync(resource.Id);

            if (existingResource == null)
            {
                return false;
            }

            bool isModified = false;

            if (existingResource.Name != resource.Name)
            {
                existingResource.Name = resource.Name;
                isModified = true;
            }

            if (existingResource.Capacity != resource.Capacity)
            {
                existingResource.Capacity = resource.Capacity;
                isModified = true;
            }

            if (existingResource.Description != resource.Description)
            {
                existingResource.Description = resource.Description;
                isModified = true;
            }

            if (existingResource.IsAvailable != resource.IsAvailable)
            {
                existingResource.IsAvailable = resource.IsAvailable;
                isModified = true;
            }

            if (existingResource.IsUnderMaintenance != resource.IsUnderMaintenance)
            {
                existingResource.IsUnderMaintenance = resource.IsUnderMaintenance;
                isModified = true;
            }

            if (existingResource.Location != resource.Location)
            {
                existingResource.Location = resource.Location;
                isModified = true;
            }

            if (isModified)
            {
                await _context.SaveChangesAsync();
            }

            return isModified;
        }

        public async Task<bool> DeleteResource(int id)
        {
            try
            {
                var resource = await _context.Resources.FindAsync(id);

                if (resource == null)
                {
                    return false;
                }

                _context.Resources.Remove(resource);

                var affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw;
            } 
        }
    }
}
