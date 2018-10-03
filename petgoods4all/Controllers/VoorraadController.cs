using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;

namespace petgoods4all.Controllers
{
    public class VoorraadController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductBrowsen()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductSearch(string productSearch)
        {
            //Db context
            var db = new ModelContext();

            var result = from voorraad in db.Voorraad select voorraad;

            return View("~/Views/Voorraad/productBrowsen.cshtml");
        }
    }
}