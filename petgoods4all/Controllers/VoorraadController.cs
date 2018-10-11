using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        
        [HttpPost]
        public ActionResult AddToShoppingCart(int identication, int Quantity)
        {
            var db = new ModelContext();
            var MaxId = 0;

            List<int> productList = new List<int>();
            productList.Add(identication);


            //add products to local shoppingcart in sessions
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

                
            //if of user ingelogd is session of met de bestaande inlog is het httpcontext.current.user
            //if (httpcontext.current.user != null){
                ShoppingCart shoppingCart = new ShoppingCart
            {
                Id = MaxId + 1,
                VoorraadId = identication,
                AccountId = 1,
                Quantity = Quantity,
            };

            db.ShoppingCart.Add(shoppingCart);
            db.SaveChanges();

            //}
            //else{
            // return View("~/Views/Account/Inloggen.cshtml");
            //}

            return ShoppingCart();
        }

        public ActionResult ShoppingCart()
        {
            var db = new ModelContext();
            //make query to find what user is logged in or get products from session

            //item uit de voorraad met prodcut id
            var result = from s in db.ShoppingCart where s.AccountId == 1 select s;

            var ShoppingCartList = result.ToList();
            //get products from voorraad db foreach productid got from result
            List<Voorraad> productList = new List<Voorraad>();

            foreach (var item in ShoppingCartList)
            {
                var voorraadResult = from v in db.Voorraad where v.Id == item.VoorraadId select v;
                foreach (var i in voorraadResult)
                {
                    productList.Add(new Voorraad()
                    {
                        Id = i.Id,
                        Naam = i.Naam,
                        Dier = i.Dier,
                        Subklasse = i.Subklasse,
                        Kwantiteit = item.Quantity,
                        Prijs = i.Prijs,
                        image = i.image
                    });
                }
            }

            Console.WriteLine(productList);
            ViewBag.ShoppingCart = productList;

            return View("~/Views/Voorraad/ShoppingCart.cshtml");
        }
    }
}