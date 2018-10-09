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
        [HttpPost]
        public ActionResult AddToShoppingCart(int productId)
        {
            var db = new ModelContext();
            var MaxId = 0;
            var UserAlreadyHasItemsInShoppingCart = 1;

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

            //var result = from s in db.ShoppingCart where userid == userid select s.Id;
            //if (!result.Any())
            //{
            //UserAlreadyHasItemsInShoppingCart = 0;
            //}
            //else
            //{
            //UserAlreadyHasItemsInShoppingCart = 1;  
            //}
            List<int> productList = new List<int>();

            productList.Add(productId);
            if (UserAlreadyHasItemsInShoppingCart == 0)
            {
                
                //if of user ingelogd is session of met de bestaande inlog is het httpcontext.current.user
                //if (httpcontext.current.user != null){
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    Id = MaxId + 1,
                    Voorraad = productList,
                    Account = 1,
                };

                db.ShoppingCart.Add(shoppingCart);
                db.SaveChanges();
            }
            else
            {
                //get existing list from db and add new productId
                var list = from s in db.ShoppingCart where s.Account == 1 select s.Voorraad;
                var voorraadList = list.ToList();
                foreach (var item in list) {
                    voorraadList.Add(item);
                }


                ShoppingCart shoppingCart = new ShoppingCart
                {
                    Id = MaxId + 1,
                    Voorraad = voorraadList,
                    Account = 1,
                };

                db.ShoppingCart.Update(shoppingCart);
                db.SaveChanges();
            }
            //}
            //else{
            //create session with list add the productid to the session
            //}

            return View("~/Views/Voorraad/ShoppingCart.cshtml");
        }

        public ActionResult ShoppingCart(int productId)
        {
            var db = new ModelContext();
            //item uit de voorraad met prodcut id
            var result = from s in db.ShoppingCart select s;

            var productList = result.ToList();

            //ViewBag.ShoppingCart = productList;

            return View();
        }
    }
}