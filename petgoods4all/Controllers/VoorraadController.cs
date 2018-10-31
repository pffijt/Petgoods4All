using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using petgoods4all.Models;
using petgoods4all.Controllers;

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

        public IActionResult SearchFilter(string DierDropdown, string SubklasseDropdown, string minPrijs, string maxPrijs, string search)
        {
            var productSearchLowerCase = search.ToLower();

            var db = new ModelContext();
            IQueryable<Voorraad> query = from voorraad in db.Voorraad where voorraad.Dier == search || voorraad.Naam == search || voorraad.Subklasse == search || voorraad.Naam.Contains(search) || voorraad.Dier.Contains(search) || voorraad.Naam.Contains(productSearchLowerCase) || voorraad.Dier.Contains(productSearchLowerCase) select voorraad;

            if (DierDropdown != null && SubklasseDropdown == null && minPrijs == null && maxPrijs == null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier));
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown != null && minPrijs == null && maxPrijs == null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse));
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown != null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown != null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs != null && maxPrijs != null)
            {
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs == null && maxPrijs != null)
            {
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs == null && maxPrijs == null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse));
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown == null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown == null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown == null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown != null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse));
                //prijs filter kan niet omdat ik niet kan parsen en er foutieve data is aangeleverd door project leider
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs == null && maxPrijs == null)
            {
                //Doe niks
            }

            var dier = (from voorraad in db.Voorraad select voorraad.Dier).Distinct();
            var dierList = dier.ToList();

            var subklasse = (from voorraad in db.Voorraad select voorraad.Subklasse).Distinct();
            var subklasseList = subklasse.ToList();

            ViewBag.Subklasse = subklasseList;
            ViewBag.Dier = dierList;
            ViewBag.SearchedString = search;

            return View("~/Views/Voorraad/ProductBrowsen.cshtml");
        }

        [HttpPost]
        public IActionResult ProductSearch(string productSearch)
        {
            //Db context
            var db = new ModelContext();

            var productSearchLowerCase = productSearch.ToLower();

            var result = from voorraad in db.Voorraad where voorraad.Dier == productSearch || voorraad.Naam == productSearch || voorraad.Subklasse == productSearch || voorraad.Naam.Contains(productSearch) || voorraad.Dier.Contains(productSearch) || voorraad.Naam.Contains(productSearchLowerCase) || voorraad.Dier.Contains(productSearchLowerCase) select voorraad;
            var productList = result.ToList();

            var dier = (from voorraad in db.Voorraad select voorraad.Dier).Distinct();
            var dierList = dier.ToList();

            var subklasse = (from voorraad in db.Voorraad select voorraad.Subklasse).Distinct();
            var subklasseList = subklasse.ToList();

            ViewBag.Subklasse = subklasseList;
            ViewBag.Dier = dierList;
            ViewBag.SearchedString = productSearch;
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
            int AccountId = 0;

            var UserId = HttpContext.Session.GetInt32("UID");
            if (UserId == null)
            {
                AccountId = 0;
            }
            else
            {
                AccountId = UserId.Value;
            }

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
            if (AccountId == 0)
            {
                return View("~/Views/Account/Inloggen.cshtml");
            }
            else
            {
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    Id = MaxId + 1,
                    VoorraadId = identication,
                    AccountId = AccountId,
                    Quantity = Quantity,
                };


                db.ShoppingCart.Add(shoppingCart);
                db.SaveChanges();
            }

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

            var UserId = HttpContext.Session.GetInt32("UID");

            //item uit de voorraad met prodcut id
            var result = from s in db.ShoppingCart where s.AccountId == UserId select s;

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
                        Id = item.Id,
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

            double c = 0;
            foreach (var item in productList)
            {
                
                double a = Convert.ToDouble(item.Prijs);
                double b = a * item.Kwantiteit;
                c = c + b;
                decimal d = Convert.ToDecimal(c / 100);
                string prijs = d.ToString("00.00");

                ViewBag.Prijs = prijs;
            }

            return View("~/Views/Voorraad/ShoppingCart.cshtml");
        }
        [HttpPost]
        public ActionResult RemoveFromShoppingCart(int productId)
        {
            var db = new ModelContext();

            var result = (from s in db.ShoppingCart where s.Id == productId select s).Single();
            
            
                db.ShoppingCart.Remove(result);
                db.SaveChanges();
            

            return ShoppingCart();
        }
    }
}