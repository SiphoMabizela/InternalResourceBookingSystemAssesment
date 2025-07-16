using IRBSapi.Models;

namespace IRBSapi.Client.Interfaces
{
    public interface IResources
    {
        public Task<List<Resource>> GetAllResources();
        public Task<bool> CreateResource(Resource resource);
        public Task<Resource> GetResourceDetailsById(int id);
        public Task<bool> UpdateResource(Resource resource);
        public Task<bool> DeleteResource(int id);
    }
}
