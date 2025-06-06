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
            presentation?.Contributors?.Append(HttpContext.Session.GetString("username"));
            return View(presentation);
        }

        public async Task<JsonResult> LoadMore()
        {
            var presentations = await _httpClient.GetFromJsonAsync<IEnumerable<Presentation>>($"Presentations/{++_batch}");
            return Json(presentations);
        }

    }
        
}
