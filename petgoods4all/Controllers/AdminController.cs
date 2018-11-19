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
        //Roept pagina op met de lijsten van Account of Product
        public ActionResult AdminKlantIndex()
        {
            using (db)
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
        //Roept pagina op met de Details van Account of Product
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
        //Roept pagina op voor het aanmaken van een Account of Product
        public ActionResult AdminCreateAccount()
        {

            return View();
        }
        public ActionResult AdminCreateVoorraad()
        {

            return View();
        }

        //Roept pagina op voor de Details van een Account of Product
        public ActionResult AdminKlantEdit()
        {
            return View();
        }



        public ActionResult AdminVoorraadEdit()
        {
            return View();
        }

        //Creeren nieuw Account/Product als je op de Opslaan knop klikt
        [HttpPost]
        public ActionResult AdminCreateAccountSave(string email,string postcode, string provincie, string huisnummer, string achternaam, string voornaam, string telefoonnummer, string straatnaam, bool Admin, string password)
        {
            using (db)
            {
                var result = from acc in db.Account select acc.id;

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
                    straatnaam = straatnaam,
                    postcode = postcode,
                    provincie = provincie,
                    huisnummer = huisnummer
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

        //Updaten Account/Product als je op de opslaan knop klikt in de Edit pagina
        public ActionResult AdminKlantEditSave(int? id, string voornaam, string huisnummer, string provincie, string postcode,  string achternaam, string email, string straatnaam, string telefoonnummer)
        {
            using (db)
            {
                var accountToUpdate = db.Account.FirstOrDefault();

                accountToUpdate.achternaam = achternaam;
                accountToUpdate.voornaam = voornaam;
                accountToUpdate.telefoonnummer = telefoonnummer;
                accountToUpdate.straatnaam = straatnaam;
                accountToUpdate.email = email;
                accountToUpdate.huisnummer = huisnummer;
                accountToUpdate.provincie = provincie;
                accountToUpdate.postcode = postcode;

                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }


        public ActionResult AdminVoorraadEditSave(int? id, string Naam, string Dier, string Subklasse, int Kwantiteit, string Prijs, string image)
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


        //Deleten Account/Product waarbij je op de knop klikt
        public ActionResult AdminKlantDelete(int? id)
        {
            using (db)
            {
                var accountToDelete = db.Account.Find(id); ;
                db.Account.Remove(accountToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }

        public ActionResult AdminVoorraadDelete(int? id)
        {
            using (db)
            {
                var voorraadToDelete = db.Voorraad.Find(id);
                db.Voorraad.Remove(voorraadToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex");
        }
    }
}


        