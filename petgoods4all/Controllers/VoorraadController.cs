﻿using System;
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
        
        public IActionResult ProductSearch(string s , int P, string DierDropdown, string SubklasseDropdown, string minPrijs, string maxPrijs)
        {
            ViewBag.a = DierDropdown;
            ViewBag.b = SubklasseDropdown;
            ViewBag.c = minPrijs;
            ViewBag.v = maxPrijs;
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
            var imageList = dier.ToList();

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

            var UserId = HttpContext.Session.GetInt32("UID").GetValueOrDefault(0);
            ViewBag.UID = UserId;

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
                string prijs = d.ToString("0.00");

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