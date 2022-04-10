using CaseApp.DataLayer.Abstract;
using CaseApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseApp.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly IContentRepository _contentRepository;

        public DataController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List(int? id)
        {
            if (id == 1)
            {
                return View(_contentRepository.GetAll().OrderByDescending(x => x.Date));
            }
            else
            {
                return View(_contentRepository.GetAll());
            }
        }
    }
}
