using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult ProductBrowsen()
        {
            ViewBag.Message = "Producten";
            return View();
        }
        public ActionResult infoPagina()
        {
            ViewBag.Message = "Information";
            return View();
        }
    }
}