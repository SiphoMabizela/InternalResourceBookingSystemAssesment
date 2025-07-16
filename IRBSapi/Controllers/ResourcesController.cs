using IRBSapi.Client.Interfaces;
using IRBSapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRBSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResources _iResources;

        public ResourcesController(IResources iResources)
        {
            _iResources = iResources;
        }

        [HttpGet]
        public async Task<List<Resource>> GetResourcesAsync() => await _iResources.GetAllResources();

        [HttpPost]
        public async Task<bool> CreateResource(Resource resource) => await _iResources.CreateResource(resource);

        [HttpGet("{id}")]
        public async Task<Resource> GetResourcesById(int id) => await _iResources.GetResourceDetailsById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, [FromBody] Resource resource)
        {
            if (id != resource.Id)
                return BadRequest();

            var success = await _iResources.UpdateResource(resource);

            if (success)
                return Ok();

            return StatusCode(500, "Failed to update");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var deleted = await _iResources.DeleteResource(id);
            if (deleted)
                return Ok();
            return StatusCode(500, "Delete failed");
        }
    }
}
