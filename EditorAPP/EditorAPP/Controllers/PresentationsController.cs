using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EditorAPP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EditorAPP.Controllers
{
    public class PresentationsController : Controller
    {
        private static int _batch = 0;
        private readonly HttpClient _httpClient;

        public PresentationsController(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetClient()!;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _httpClient.GetFromJsonAsync<IEnumerable<Presentation>>($"Presentations/GetByBatch/{0}"));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var presentation = await _httpClient.GetFromJsonAsync<Presentation>($"Presentations/GetById/{id}");
            return View(presentation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presentation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Presentation presentation)
        {
            var response = await _httpClient.PostAsJsonAsync($"Presentations/Create", presentation);
            if (response.IsSuccessStatusCode)
            {
                ViewData["CreateMessage"] = "Presentation created successfully.";
                return View("Index");
            }
            ViewData["CreateMessage"] = "Creating error occured";
            return View("Index");
        }

        public async Task<JsonResult> LoadMore()
        {
            var presentations = await _httpClient.GetFromJsonAsync<IEnumerable<Presentation>>($"Presentations/GetByBatch/{++_batch}");
            return Json(presentations);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]Presentation presentation)
        {
            if (presentation != null)
            {
                var response = await _httpClient.PutAsJsonAsync($"Presentations/Update", presentation);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["PresentationMessage"] = "Presentation updated successfully.";
                    return View("Edit", presentation);
                }
                ViewData["PresentationMessage"] = "Updating error occured";
                return View("Edit", presentation);
            }
            else
            {
                ViewData["PresentationMessage"] = "Presentation data is null or invalid.";
                return View("Edit", presentation);
            }
               
        }

    }
        
}
