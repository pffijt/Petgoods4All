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
        
        public ActionResult AddToShoppingCart(int identication)
        {
            var db = new ModelContext();
            var MaxId = 0;

            //check if user already has items in ShoppingCart
            //add quantity to shoppingcart

            //check max Id
            var result = from s in db.ShoppingCart select s.Id;
            if (!result.Any())
            {
            }
            else
            {
                MaxId = result.Max();
            }

            //check if this user already has items in shoppingcart
                
            //if of user ingelogd is session of met de bestaande inlog is het httpcontext.current.user
            //if (httpcontext.current.user != null){
                ShoppingCart shoppingCart = new ShoppingCart
            {
                Id = MaxId + 1,
                VoorraadId = identication,
                AccountId = 1,
            };

            db.ShoppingCart.Add(shoppingCart);
            db.SaveChanges();
            
            //}
            //else{
            //create session with list add the productid to the session
            //}

            return View("~/Views/Voorraad/ShoppingCart.cshtml");
        }

        public ActionResult ShoppingCart(int productId)
        {
            var db = new ModelContext();
            //make query to find what user is logged in or get products from session

            //item uit de voorraad met prodcut id
            var result = from s in db.ShoppingCart where s.AccountId == 1 select s.VoorraadId;

            //get products from voorraad db foreach productid got from result

            var productList = result.ToList();

            ViewBag.ShoppingCart = productList;

            return View();
        }
    }
}