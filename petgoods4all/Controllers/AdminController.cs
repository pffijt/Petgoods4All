using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Extensions;
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

        public ActionResult AdminCreateAccount()
        {

            return View();
        }

        public ActionResult AdminKlantEdit()
        {
            return View();
        }

        public ActionResult AdminCreateVoorraad()
        {

            return View();
        }

        public ActionResult AdminVoorraadEdit()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AdminCreateAccountSave(string email, string achternaam, string voornaam, string telefoonnummer, string straatnaam, bool Admin, string password)
        {
            using (db)
            {
                var result = from acc in db.Account select acc.id
                var MaxId = result.Max();

                Account a = new Account
                {
                    id = MaxId + 1,
                    email = email,
                    Admin = false,
                    password = password,
                    voornaam = voornaam,
                    achternaam = achternaam,
                    telefoonnummer = telefoonnummer,
                    straatnaam = straatnaam
                };
                db.Account.Add(a);
                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }

        public ActionResult AdminCreateVoorraadSave(string Naam, string Dier, string Subklasse, string Prijs, int Kwantiteit)
        {
            using (db)
            {
                var result = from acc in db.Account select acc.id;

                var MaxId = result.Max();

                Voorraad a = new Voorraad
                {
                    Id = MaxId + 1,
                    Naam = Naam,
                    Dier = Dier,
                    Subklasse = Subklasse,
                    Prijs = Prijs,
                    Kwantiteit = Kwantiteit
                };
                db.Voorraad.Add(a);
                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex");
        }

        
        public ActionResult AdminKlantEditSave(int? id, string voornaam, string achternaam, string email, string straatnaam, string telefoonnummer)
        {
            using (db)
            {
                var accountToUpdate = db.Account.FirstOrDefault();

                accountToUpdate.achternaam = achternaam;
                accountToUpdate.voornaam = voornaam;
                accountToUpdate.telefoonnummer = telefoonnummer;
                accountToUpdate.straatnaam = straatnaam;
                accountToUpdate.email = email;

                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }


        public  ActionResult AdminVoorraadEditSave(int? id, string Naam, string Dier, string Subklasse, int Kwantiteit, string Prijs, string image)
        {
            using (db)
            {
                var voorraadToUpdate = db.Voorraad.FirstOrDefault();

                voorraadToUpdate.Naam = Naam;
                voorraadToUpdate.Dier = Dier;
                voorraadToUpdate.Subklasse = Subklasse;
                voorraadToUpdate.Kwantiteit = Kwantiteit;
                voorraadToUpdate.Prijs = Prijs;
                voorraadToUpdate.image = image;

                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex"); 
        }
        
        
        public ActionResult AdminKlantDelete(int? id)
        {
            using (db)
            {
                var accountToDelete = db.Account.FirstOrDefault();
                db.Account.Remove(accountToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }

        [HttpDelete]
        public ActionResult AdminVoorraadDelete(int? id)
        {
            using (db)
            {
                var voorraadToDelete = db.Voorraad.FirstOrDefault();
                db.Voorraad.Remove(voorraadToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex");
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