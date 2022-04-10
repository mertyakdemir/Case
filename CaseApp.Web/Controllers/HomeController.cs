using CaseApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using CaseApp.Entities;
using CaseApp.DataLayer.Abstract;
using CaseApp.Web.Models.DTOs;

namespace CaseApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IContentRepository _contentRepository;

        public HomeController(ILogger<HomeController> logger, IContentRepository contentRepository)
        {
            _logger = logger;
            _contentRepository = contentRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(ContentViewModel contentViewModel)
        {
            string translate = "";
            var model = new TextModel() { text = contentViewModel.PText };
            var stringPayload = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var res = await client.PostAsync($"https://api.funtranslations.com/translate/{contentViewModel.PSpeak}", httpContent);
                
                if (res.Content != null)
                {
                    var responseContent = res.Content.ReadAsStringAsync();
                    string jsonString = responseContent
                                               .Result
                                               .Replace("\\", "")
                                               .Trim(new char[1] { '"' });

                    Response result =  JsonConvert.DeserializeObject<Response>(jsonString);
                    var newContent = new ContentModel()
                    {
                        Translated = result.contents.translated,
                        Text = result.contents.text,
                        Translation = result.contents.translation,
                        Date = DateTime.Now
                    };
                    _contentRepository.Create(newContent);
                    translate = newContent.Translated;
                }
            }

            ViewBag.Text = translate;
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
