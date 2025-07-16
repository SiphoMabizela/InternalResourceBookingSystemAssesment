using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IRBSui.Models;
using System.Text;

namespace IRBSui.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5042/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            List<Resource> resources = new List<Resource>();
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Resources/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                resources = JsonConvert.DeserializeObject<List<Resource>>(jsonData);
            }
            return View(resources);
        }

        public ActionResult Create()
        {
            return PartialView("_CreateResourcePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Resource model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateResourcePartial", model);
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8,"application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/Resources", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Recourse created successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create resource.");
            return PartialView("_CreateResourcePartial", model);
        }

        public async Task<ActionResult> DetailsPartial(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Resources/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var resource = JsonConvert.DeserializeObject<Resource>(json);

            return PartialView("_DetailsModalPartial", resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Resource model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DetailsModalPartial", model);
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Resources/{model.Id}", jsonContent);
            
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Recourse updated successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update resource.");
            return PartialView("_DetailsModalPartial", model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Resources/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Resource deleted successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Failed to delete the resource.";
            return RedirectToAction("Index");
        }
    }
}