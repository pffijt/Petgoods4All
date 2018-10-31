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
        ModelContext db = new ModelContext();
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


        
        public ActionResult ReadProduct(string inputAdminProductNaam)
        {
            Voorraad product = db.Voorraad.Find(inputAdminProductNaam);

            return AdminProductbeheer();
        }


        public ActionResult ReadAccount(string inputAdminKlantMail)
        {
            Account account = db.Account.Find(inputAdminKlantMail);
            

            return AdminKlantbeheer(); 
        }

        public ActionResult AdminKlantIndex()
        {
            using(db)
            {
                var accounts = db.Account.ToList();

            return View(accounts);
            }
        }

        public ActionResult AdminVoorraadIndex()
        {
            using (db)
            {
                var voorraad = db.Voorraad.ToList();

                return View(voorraad);
            }
        }

        public ActionResult AdminKlantDetails(int? id)
        {

            Account account = db.Account.Find(id);

            return View(account);
        }

        public ActionResult AdminVoorraadDetails(int? id)
        {

            Voorraad voorraad = db.Voorraad.Find(id);

            return View(voorraad);
        }


        [HttpPost]
        
        


        public ActionResult AdminCreateAccount(string inputAdminKlantVoorNm, string inputAdminKlantAchterNaam, string inputAdminKlantMail, string inputAdminKlantTel, string inputAdminKlantAdres, string inputAdminKlantWw)
        {
            Account a = new Account
            {

                email = inputAdminKlantMail,
                Admin = false,
                password = inputAdminKlantWw,
                voornaam = inputAdminKlantVoorNm,
                achternaam = inputAdminKlantAchterNaam,
                telefoonnummer = inputAdminKlantTel,
                straatnaam = inputAdminKlantAdres
            };
            db.Account.Add(a);
            db.SaveChanges();
            return View();
        }

        public ActionResult AdminKlantEdit(int? id)
        {
            
            var accountToUpdate = db.Account.Find(id);

            db.SaveChanges();

                    return View();
        }

        public ActionResult AdminVoorraadEdit(int? id)
        {

            var voorraadToUpdate = db.Voorraad.Find(id);

            db.SaveChanges();

            return View();
        }

        public ActionResult AdminKlantDelete(int? id)
        {
            var accountToDelete = db.Account.Find(id);
            db.Account.Remove(accountToDelete);
            db.SaveChanges();
            return RedirectToAction("AdminKlantIndex");
        }

        public ActionResult AdminVoorraadDelete(int? id)
        {
            var voorraadToDelete = db.Voorraad.Find(id);
            db.Voorraad.Remove(voorraadToDelete);
            db.SaveChanges();
            return RedirectToAction("AdminVoorraadIndex");
        }


        public ActionResult CreateProduct(string inputAdminProductNaam, string inputAdminProductDier, string inputAdminProductSuptype, string inputAdminProductPrijs, int inputAdminProductKwantiteit )
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