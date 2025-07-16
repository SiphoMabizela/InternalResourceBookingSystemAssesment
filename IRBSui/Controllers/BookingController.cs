using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IRBSui.Models;
using System.Text;

namespace IRBSui.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5042/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            List<Booking> bookings = new List<Booking>();
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Bookings/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                bookings = JsonConvert.DeserializeObject<List<Booking>>(jsonData);
            }
            return View(bookings);
        }

        public async Task<ActionResult> Create()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/ResourceBooking");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<ResourceBooking>>(jsonData);

                ViewBag.AvailableResources = new SelectList(bookings, "Id", "Name");
            }
            else
            {
                ViewBag.AvailableResources = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            return PartialView("_CreateBookingPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Booking model)
        {
            if (!ModelState.IsValid)
            {
                var response = await _httpClient.GetAsync("/api/ResourceBooking");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var bookings = JsonConvert.DeserializeObject<List<ResourceBooking>>(jsonData);
                    ViewBag.AvailableResources = new SelectList(bookings, "Id", "Name");
                }
                return PartialView("_CreateBookingPartial", model);
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage responseCreate = await _httpClient.PostAsync("api/Bookings", jsonContent);

            if (responseCreate.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Booking created successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create booking.");
            return PartialView("_CreateBookingPartial", model);
        }

        public async Task<ActionResult> DetailsPartial(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Bookings/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<Booking>(json);

            HttpResponseMessage responseD = await _httpClient.GetAsync("/api/ResourceBooking");
            if (responseD.IsSuccessStatusCode)
            {
                var jsonData = await responseD.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<ResourceBooking>>(jsonData);

                ViewBag.AvailableResources = new SelectList(bookings, "Id", "Name");
            }

            return PartialView("_DetailsBookingModalPartial", booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Booking model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DetailsBookingModalPartial", model);
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Bookings/{model.Id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Booking updated successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update resource.");
            return PartialView("_DetailsBookingModalPartial", model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Bookings/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Booking deleted successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Failed to delete the booking.";
            return RedirectToAction("Index");
        }
    }
}