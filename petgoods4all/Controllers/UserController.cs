using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;

namespace petgoods4all.Controllers
{
    public class UserController : Controller
    {

        // GET: User
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
            return View();
        }
        
        //
        public ActionResult ReadAccount(string inputEmailUser)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(inputEmailUser);

            return View(account);
        }

        public ActionResult UpdateAccount(string inputEmailUser, string newEmail, string newinputPasswordUser)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(inputEmailUser);
            account.email = newEmail;
            account.password = newinputPasswordUser;
            db.Account.Update(account);

            return View(account);
        }

        public ActionResult DeleteAccount(string inputEmailUser)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(inputEmailUser);
            db.Account.Remove(account);

            return View();
        }
    }
}