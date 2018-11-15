using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;

namespace petgoods4all.Controllers
{
    public class UserController : Controller
    {
        /////////inlog page////////
        // GET: User
        ModelContext db = new ModelContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult UserBeheer()
        {
             // var UserId = HttpContext.Session.GetInt32("UID");
            var userId = HttpContext.Session.GetInt32("UID");
            ViewBag.userId = userId;
            return View();
        }
        
        //
        public ActionResult ReadAccount(string inputEmailUser)
        {
            Account account = db.Account.Find(inputEmailUser);

            return View(account);
        }

        public ActionResult UpdateAccount(string inputEmailUser, string originalEmailUser, string inputPasswordUser, string inputVoornaamUser, string inputAchternaamUser, string inputTelefoonnummerUser, string inputStraatnaamUser)
        {            
            using (db)
            {
                //var accountUpdate = db.Account.Find(inputEmailUser);// input from inlog, comparing db email.
                var accountUpdate = db.Account.Where(account => account.email == originalEmailUser).FirstOrDefault();
                accountUpdate.voornaam = inputVoornaamUser;
                accountUpdate.achternaam = inputAchternaamUser;
                accountUpdate.telefoonnummer = inputTelefoonnummerUser;
                accountUpdate.straatnaam = inputStraatnaamUser;
                accountUpdate.password = inputPasswordUser;
                accountUpdate.email = inputEmailUser;

                db.Account.Update(accountUpdate);
                db.SaveChanges();
            }
            return RedirectToAction("UserHome");
        }

        public ActionResult DeleteAccount(string inputEmailUser)
        {
            Account account = db.Account.Find(inputEmailUser);
            db.Account.Remove(account);

            return View();
        }        
    }
}