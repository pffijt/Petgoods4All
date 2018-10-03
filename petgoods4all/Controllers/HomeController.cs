using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;


namespace petgoods4all.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Your ShoppingCart page.";

            return View();
        }

        public ActionResult Productpage()
        {
            ViewBag.Message = "Product page";
            return View();
        }

        public ActionResult Wishpage()
        {
            ViewBag.Message = "This is your wishpage.";

            return View();
        }
        public IActionResult ProductBrowsen(string D, int P = 1)
        {
            ViewBag.Message = "Producten";
            using (var db = new ModelContext())
            {
                //pagination per soort
                ViewBag.firstnum = 1+(P*16)-16;
                ViewBag.secondnum = 16*P;
                ViewBag.paginationindex = P;
                ViewBag.animal = D;
                var goods = from m in db.Voorraad where m.Dier == D select m;
                ViewBag.count = goods.Count();
                if(ViewBag.secondnum > ViewBag.count)
                {
                    ViewBag.secondnum = ViewBag.count;
                }
                var goodsList = goods.Skip(((P*16)-16)).Take(16).ToList();
                return View(goodsList);
            }
        }
        public ActionResult infoPagina()
        {
            ViewBag.Message = "Information";
            return View();
        }
    }
}