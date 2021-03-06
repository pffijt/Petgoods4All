﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using petgoods4all.Models;
using petgoods4all.Controllers;
using System.Globalization;

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
        
        public IActionResult ProductSearch(string s , int P, string DierDropdown, string SubklasseDropdown, string minPrijs, string maxPrijs)
        {
            ViewBag.a = DierDropdown;
            ViewBag.b = SubklasseDropdown;
            ViewBag.c = minPrijs;
            ViewBag.d = maxPrijs;
            ViewBag.firstnum = (P*16)-15;
            ViewBag.secondnum = 16*P;
            ViewBag.paginationindex = P;

            double MinimalePrijs = Convert.ToDouble(minPrijs);
            double MaximalePrijs = Convert.ToDouble(maxPrijs);

            var db = new ModelContext();

            var productSearchLowerCase = s.ToLower();

            IQueryable<Voorraad> query = from voorraad in db.Voorraad where voorraad.Dier == s || voorraad.Dier == productSearchLowerCase || voorraad.Naam == s || voorraad.Naam == productSearchLowerCase || voorraad.Subklasse == s || voorraad.Subklasse == productSearchLowerCase || voorraad.Subklasse.Contains(s) || voorraad.Subklasse.Contains(productSearchLowerCase) || voorraad.Naam.Contains(s) || voorraad.Dier.Contains(s) || voorraad.Naam.Contains(productSearchLowerCase) || voorraad.Dier.Contains(productSearchLowerCase) select voorraad;

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
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse) && Convert.ToDouble(p.Prijs) > MinimalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown != null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse) && Convert.ToDouble(p.Prijs) > MinimalePrijs && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse) && Convert.ToDouble(p.Prijs) > MinimalePrijs && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => Convert.ToDouble(p.Prijs) > MinimalePrijs && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs == null && maxPrijs == null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse));
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse) && Convert.ToDouble(p.Prijs) > MinimalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => Convert.ToDouble(p.Prijs) > MinimalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown == null && minPrijs != null && maxPrijs == null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && Convert.ToDouble(p.Prijs) > MinimalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown == null && minPrijs != null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && Convert.ToDouble(p.Prijs) > MinimalePrijs && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown == null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown != null && SubklasseDropdown != null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => DierDropdown.Contains(p.Dier) && SubklasseDropdown.Contains(p.Subklasse) && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown != null && minPrijs == null && maxPrijs != null)
            {
                query = query.Where(p => SubklasseDropdown.Contains(p.Subklasse) && Convert.ToDouble(p.Prijs) < MaximalePrijs);
                ViewBag.Voorraad = query;
            }
            else if (DierDropdown == null && SubklasseDropdown == null && minPrijs == null && maxPrijs == null)
            {
                //Doe niks
            }
            var result = query.Skip(((P*16)-16)).Take(16);
            var productList = result.ToList();
            ViewBag.count = query.Count();
            if(ViewBag.secondnum > ViewBag.count)
            {
                ViewBag.secondnum = ViewBag.count;
            }

            var dier = (from voorraad in db.Voorraad select voorraad.Dier).Distinct();
            var dierList = dier.ToList();

            var image = (from voorraad in db.Voorraad select voorraad.image).Distinct();
            var imageList = image.ToList();

            var subklasse = (from voorraad in db.Voorraad select voorraad.Subklasse).Distinct();
            var subklasseList = subklasse.ToList();

            ViewBag.Subklasse = subklasseList;
            ViewBag.Dier = dierList;
            ViewBag.image = imageList;
            ViewBag.SearchedString = s;
            ViewBag.Voorraad = productList;
            return View();
        }

      

        [HttpPost]
        public IActionResult Search (string productSearch, string DierDropdown, string SubklasseDropdown, string minPrijs, string maxPrijs)
        {

            return RedirectToAction("ProductSearch","Voorraad", new { s=productSearch, P = "1",DierDropdown = DierDropdown, 
            SubklasseDropdown =SubklasseDropdown, minPrijs= minPrijs,maxPrijs=maxPrijs });
        }
        
        [HttpPost]
        public ActionResult AddToShoppingCart(int identication, int Quantity)
        {
            var db = new ModelContext();
            var MaxId = 0;

            List<int> productList = new List<int>();
            productList.Add(identication);
            int? AccountId = 0;

            var UserId = HttpContext.Session.GetInt32("UID");
            if (UserId == null)
            {
                var AccountResult = from r in db.Account select r.id;
                //Random rnd = new Random();
                //var randomAccountId = rnd.Next(skrr.Max(), 1000000000);
                int? randomAccountIdSum = AccountResult.Max() + 1;
                int randomAccountId = randomAccountIdSum.GetValueOrDefault();
                var AccountSession = HttpContext.Session.GetInt32("SessionAccountId");
                if (AccountSession == null)
                {
                    HttpContext.Session.SetInt32("SessionAccountId", randomAccountId);

                }
                if (!AccountResult.Contains(randomAccountId))
                {
                    AccountId = HttpContext.Session.GetInt32("SessionAccountId");
                }
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
            //if (AccountId == 0)
            //{
            //    return View("~/Views/Account/Inloggen.cshtml");
            //}
            //else
            //{
            var checkAlreadyIn = (from s in db.ShoppingCart where identication == s.VoorraadId && UserId == s.AccountId select s).Any();
            if(checkAlreadyIn)
            {
                var MogelijkGetal = (from a in db.Voorraad where identication == a.Id select a.Kwantiteit).Single();
                var eerstGetal = (from b in db.ShoppingCart where identication == b.VoorraadId && UserId == b.AccountId select b.Quantity).Single();
                if(eerstGetal+Quantity <= MogelijkGetal)
                {
                    (from s in db.ShoppingCart where identication == s.VoorraadId && UserId == s.AccountId select s)
                    .ToList().ForEach(s => s.Quantity = s.Quantity+Quantity);
                }
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
            }

            db.SaveChanges();
            //}

            //}
            //else{
            // return View("~/Views/Account/Inloggen.cshtml");
            //}

            return RedirectToAction("ShoppingCart", "Voorraad");
        }

        public ActionResult ShoppingCart()
        {
            var db = new ModelContext();
            var culture = new System.Globalization.CultureInfo("nl-NL");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            Console.WriteLine("Cultureinfo : "+CultureInfo.CurrentCulture);
            //make query to find what user is logged in or get products from session
            
            var UserId = HttpContext.Session.GetInt32("UID");
            var MaxReached = HttpContext.Session.GetInt32("MaxReached"); 
            if(MaxReached != null)
            {
                var selectProduct = (from r in db.Voorraad where MaxReached == r.Id select r.Naam).Single();
                var selectAmount = (from r in db.Voorraad where MaxReached == r.Id select r.Kwantiteit).Single();
                ViewBag.SP = selectProduct;
                ViewBag.SA = selectAmount;
            }
            HttpContext.Session.Remove("MaxReached");
           
            if (UserId == null) {
                ViewBag.AnonymousU = true;
                var AccountSession = HttpContext.Session.GetInt32("SessionAccountId");

                if (AccountSession == null) {
                    var AccountResult = from r in db.Account select r.id;
                    //Random rnd = new Random();
                    //var randomAccountId = rnd.Next(skrr.Max(), 1000000000);
                    int? randomAccountIdSum = AccountResult.Max() + 1;
                    int randomAccountId = randomAccountIdSum.GetValueOrDefault();
                    AccountSession = HttpContext.Session.GetInt32("SessionAccountId");
                    if (AccountSession == null)
                    {
                        HttpContext.Session.SetInt32("SessionAccountId", randomAccountId);
                    }
                    if (!AccountResult.Contains(randomAccountId))
                    {
                        AccountSession = HttpContext.Session.GetInt32("SessionAccountId");
                    }
                    UserId = AccountSession;
                }
                UserId = AccountSession;
                
            }
            else
            {
                ViewBag.AnonymousU = false;
            }
            var _emptylist = (from s in db.ShoppingCart where s.AccountId == UserId  select s).ToList();
            if(_emptylist.Any())
            {
                //item uit de voorraad met prodcut id
                var result = from s in db.ShoppingCart where s.AccountId == UserId select s;
                var ShoppingCartList = result.ToList();
                //get products from voorraad db foreach productid got from result
                List<Voorraad> productList = new List<Voorraad>();
            
                ViewBag.emptylist = false;
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
                    double a = double.Parse(item.Prijs );
                    double b = a * item.Kwantiteit;
                    c = c + b;
                }
                decimal d = Convert.ToDecimal(c/100, new System.Globalization.CultureInfo("en-US"));
                string prijs = d.ToString("F",new System.Globalization.CultureInfo("en-US"));
                ViewBag.Prijs = prijs;
                 return View();
            }
            else
            {
                ViewBag.emptylist = true;
                return View();
            }
        }
        [HttpPost]
        public ActionResult RemoveFromShoppingCart(int productId)
        {
            var db = new ModelContext();

            var result = (from s in db.ShoppingCart where s.Id == productId select s).Single();
            
            
                db.ShoppingCart.Remove(result);
                db.SaveChanges();


            return RedirectToAction("ShoppingCart", "Voorraad");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int NewQuantity)
        {
            var db = new ModelContext();

            var UserId = HttpContext.Session.GetInt32("UID");
            var UUserId = HttpContext.Session.GetInt32("SessionAccountId");
            
            var checkProductIDVoorraad = (from s in db.ShoppingCart where s.Id == productId select s.VoorraadId).Single();
            var QuantityPossible = (from q in db.Voorraad where q.Id == checkProductIDVoorraad && NewQuantity <= q.Kwantiteit select q).Any();
            if(QuantityPossible)
            {
                var result = (from r in db.ShoppingCart where r.Id == productId && (r.AccountId == UserId || r.AccountId == UUserId) select r).ToList();
                foreach(var item in result)
                {
                    item.Quantity = NewQuantity;
                }

                db.SaveChanges();
            }
            else
            {
                HttpContext.Session.SetInt32("MaxReached",checkProductIDVoorraad);
            }
            
            return RedirectToAction("ShoppingCart", "Voorraad");
        }
    }
}