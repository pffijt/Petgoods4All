﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;
using Microsoft.AspNetCore.Http;

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

        public ActionResult Inloggen()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }

        [HttpPost]
        public ActionResult Aanmelden(string inputEmail, string inputPassword, string confirmPassword)
        {
            int MaxId;
            var email = inputEmail;
            var password = inputPassword;
            var db = new ModelContext();
            var result = from acc in db.Account select acc.id;
            if( !result.Any() )
            {
                MaxId = 0;
            }
            else
            {
                MaxId = result.Max();
            }

            Account a = new Account
            {
                id = MaxId + 1,
                email = inputEmail,
                password = confirmPassword
            };

            db.Account.Add(a);
            db.SaveChanges();
            
            return Redirect("Inloggen");
        }
        [HttpPost]
        public ActionResult Inloggen(string inputEmail, string inputPassword)
        {
            using (var db = new ModelContext())
            {
                var cilc = from acc in db.Account where inputEmail == acc.email select acc.email;
                if(cilc.Any() == true)
                {
                    var resultEmail = (from acc in db.Account where inputEmail == acc.email select acc.email).Single();
                    var resultId = (from acc in db.Account where inputEmail == acc.email select acc.id).Single();
                    var resultPassword = (from acc in db.Account where inputPassword == acc.password select acc.password).Single();
                    var resultAdmin = (from acc in db.Account where inputEmail == acc.email select acc.Admin).Single();
                    int userId = (from r in db.Account where r.email==inputEmail   select r.id).Single();
                    if (resultEmail == inputEmail && resultPassword == inputPassword)
                    {
                        HttpContext.Session.SetInt32("UserIdKey", resultId);
                        HttpContext.Session.SetString("resultEmail", resultEmail);
                        
                        if (resultAdmin == true)
                        {
                            HttpContext.Session.SetInt32("UID",userId);
                            return View("~/Views/Home/About.cshtml");
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("UID",userId);
                            return View("~/Views/Home/Index.cshtml");
                        }
                    }
                    else
                    {
                        return View("~/Views/Account/Inloggen.cshtml");
                    }
                }
                else
                {
                    return View("~/Views/Account/Inloggen.cshtml");
                }
            }
        }
    }
}