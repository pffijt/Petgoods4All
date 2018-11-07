using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;
using Microsoft.EntityFrameworkCore;


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
        public ActionResult AdminCreateAccountSave(string inputAdminKlantVoorNm, string inputAdminKlantAchterNaam, string inputAdminKlantMail, string inputAdminKlantTel, string inputAdminKlantAdres, string inputAdminKlantWw)
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
            return RedirectToPage("./Admin/AdminKlantIndex");
        }
      
        
        public ActionResult AdminKlantEditSave(int? id, string newvoornaam, string newachternaam, string newemail, string newstraatnaam, string newtelefoonnummer)
        {
            var accountToUpdate = db.Account.Find(id);

            accountToUpdate.achternaam = newachternaam;
            accountToUpdate.voornaam = newvoornaam;
            accountToUpdate.telefoonnummer = newtelefoonnummer;
            accountToUpdate.straatnaam = newstraatnaam;
            accountToUpdate.email = newemail;
            

                db.SaveChanges();

            return RedirectToPage("./Admin/AdminKlantIndex");
        }


        public  ActionResult AdminVoorraadEditSave(int? id, string newNaam, string newDier, string newSubklasse, int newKwantiteit, string newPrijs, string newimage)
        {

            var voorraadToUpdate = db.Voorraad.Find(id);

            voorraadToUpdate.Naam = newNaam;
            voorraadToUpdate.Dier = newDier;
            voorraadToUpdate.Subklasse = newSubklasse;
            voorraadToUpdate.Kwantiteit = newKwantiteit;
            voorraadToUpdate.Prijs = newPrijs;
            voorraadToUpdate.image = newimage;

                db.SaveChanges();

            return RedirectToPage("./Admin/AdminVoorradIndex");
        }

        


        public ActionResult AdminCreateVoorraadSave(string inputAdminProductNaam, string inputAdminProductDier, string inputAdminProductSuptype, string inputAdminProductPrijs, int inputAdminProductKwantiteit )
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
        [HttpDelete]
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