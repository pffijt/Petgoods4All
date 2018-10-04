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

            var productSearchLowerCase = productSearch.ToLower();

            var result = from voorraad in db.Voorraad where voorraad.Dier == productSearch || voorraad.Naam == productSearch || voorraad.Subklasse == productSearch || voorraad.Naam.Contains(productSearch) || voorraad.Dier.Contains(productSearch) || voorraad.Naam.Contains(productSearchLowerCase) || voorraad.Dier.Contains(productSearchLowerCase) select voorraad;
            var productList = result.ToList();

            ViewBag.Voorraad = productList;

            return View("~/Views/Voorraad/productBrowsen.cshtml");
        }
    }
}