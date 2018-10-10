using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;

namespace petgoods4all.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin
        [HttpGet]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult AdminInstellingen()
        {
            return View();
        }

        public ActionResult AdminKlantbeheer()
        { 
            return View();
        }

        public ActionResult AdminProductbeheer()
        {
            return View();
        }


        public ActionResult CreateAccount(string inputAdminKlantVoorNm, string inputAdminKlantAchterNaam, string inputAdminKlantMail, int inputAdminKlantTel, string inputAdminKlantAdres, string inputAdminKlantWw)
        {
            var db = new ModelContext();
            

            Account a = new Account
            {
                
                email = inputAdminKlantMail,
                Admin = false,
                password = inputAdminKlantWw,
                Voornaam = inputAdminKlantVoorNm,
                Achternaam = inputAdminKlantAchterNaam,
                Telnr = inputAdminKlantTel,
                Straatnaam = inputAdminKlantAdres
            };
            db.Account.Add(a);
            db.SaveChanges();
            return View();
        }



        public ActionResult ReadAccount(string inputAdminKlantMail)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(inputAdminKlantMail);
            

            return View(account);
        }

        public ActionResult UpdateAccount(string inputAdminKlantMail, string newEmail)
        { 
            var db = new ModelContext();
            Account account = db.Account.Find(inputAdminKlantMail);

            account.email = newEmail;
            
            db.Account.Update(account);
            return View(account);
        }

        public ActionResult DeleteAccount(string inputAdminKlantMail)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(inputAdminKlantMail);
            db.Account.Remove(account);
            return View();
        }

        public ActionResult CreateProduct(string inputAdminProductNaam, string inputAdminProductDier, string inputAdminProductSuptype, double inputAdminProductPrijs, int inputAdminProductKwantiteit )
        {
            var db = new ModelContext();
            

            Voorraad a = new Voorraad
            {
                Naam = inputAdminProductNaam,
                Dier = inputAdminProductDier,
                Subklasse = inputAdminProductSuptype,
                Prijs = inputAdminProductPrijs,
                Kwantiteit = inputAdminProductKwantiteit
            };
            db.Voorraad.Add(a);
            db.SaveChanges();
            return View();
        }

            public ActionResult ReadProduct(string inputAdminProductNaam)
        {
            var db = new ModelContext();
            Voorraad product = db.Voorraad.Find(inputAdminProductNaam);

            return View(product);
        }

        public ActionResult UpdateProduct(string inputAdminProductNaam, string newNaam, string newDier, string newSubtype, int newKwantiteit, double newPrijs  )
        {
            var db = new ModelContext();
            Voorraad product = db.Voorraad.Find(inputAdminProductNaam);
            product.Dier = newDier;
            product.Kwantiteit = newKwantiteit;
            product.Naam = newNaam;
            product.Subklasse = newSubtype;
            product.Prijs = newPrijs;
            return View(product);
        }
        public ActionResult DeleteProduct(string inputAdminProductNaam)
        {
            var db = new ModelContext();
            Voorraad product = db.Voorraad.Find(inputAdminProductNaam);
            db.Voorraad.Remove(product);
            return View();
        }
            /*[HttpPost]
            public ActionResult Aanmelden(string inputEmail, string inputPassword, string confirmPassword)
            {
                var email = inputEmail;
                var password = inputPassword;

                var db = new ModelContext();

                var result = from acc in db.Account select acc.id;

                var MaxId = result.Max();

                Account a = new Account
                {
                    id = MaxId + 1,
                    email = inputEmail,
                    password = confirmPassword
                };

                db.Account.Add(a);
                db.SaveChanges();

                return View("~/Views/Account/Inloggen.cshtml");
            }*/
        }
}