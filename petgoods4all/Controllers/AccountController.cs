using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petgoods4all.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Aanmelden()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Aanmelden(string inputEmail, string inputPassword, string confirmPassword)
        {
            var email = inputEmail;
            var password = inputPassword;

            return View();
        }
    }
}