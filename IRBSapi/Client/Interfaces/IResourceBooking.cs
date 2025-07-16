using IRBSapi.Models;

namespace IRBSapi.Client.Interfaces
{
    public interface IResourceBooking
    {
        public Task<List<ResourceBooking>> GetAllResourceBooking();
    }
}
