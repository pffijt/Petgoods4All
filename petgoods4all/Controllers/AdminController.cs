using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;
using PagedList;

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


        
        public ActionResult ReadProduct(string searchName)
        {
            var db = new ModelContext();
            Voorraad product = db.Voorraad.Find(searchName);

            return View("~/Views/Admin/AdminProductbeheer.cshtml");
        }


        public ActionResult ReadAccount(string searchEmail)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(searchEmail);


            return View("~/Views/Admin/AdminKlantbeheer.cshtml");
        }


        public ActionResult ReadIndexAccounts(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var db = new ModelContext();
            

                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Achternaam_desc" : "";


                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;


                var accounts = from s in db.Account
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    accounts = accounts.Where(s => s.Achternaam.Contains(searchString)
                                           || s.Voornaam.Contains(searchString)
                                           || s.Gemeente.Contains(searchString)
                                           || s.email.Contains(searchString)
                                           );
                }
                switch (sortOrder)
                {
                    case "Achternaam_desc":
                        accounts = accounts.OrderByDescending(s => s.Achternaam);
                        break;
                    case "Voornaam":
                        accounts = accounts.OrderBy(s => s.Voornaam);
                        break;
                    case "Voornaam_desc":
                        accounts = accounts.OrderByDescending(s => s.Voornaam);
                        break;
                    case "Gemeente":
                        accounts = accounts.OrderBy(s => s.Gemeente);
                        break;
                    case "Gemeente_desc":
                        accounts = accounts.OrderByDescending(s => s.Gemeente);
                        break;
                    default:  // Name ascending 
                        accounts = accounts.OrderBy(s => s.Achternaam);
                        break;
                }


            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }




        [HttpPost]
        public ActionResult UpdateAccount(string inputAdminKlantMail, string newEmail)
        { 
            var db = new ModelContext();
            Account account = db.Account.Find(inputAdminKlantMail);

            account.email = newEmail;
            
            db.Account.Update(account);
            return View("~/Views/Admin/AdminKlantbeheer.cshtml");
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
            return View("~/Views/Admin/AdminKlantbeheer.cshtml");
        }

        public ActionResult DeleteAccount(string searchEmail)
        {
            var db = new ModelContext();
            Account account = db.Account.Find(searchEmail);
            db.Account.Remove(account);
            return View("~/Views/Admin/AdminKlantbeheer.cshtml");
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
            return View("~/Views/Admin/AdminProductbeheer.cshtml");
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
            return View("~/Views/Admin/AdminProductbeheer.cshtml");
        }
        public ActionResult DeleteProduct(string searchName)
        {
            var db = new ModelContext();
            Voorraad product = db.Voorraad.Find(searchName);
            db.Voorraad.Remove(product);
            return View("~/Views/Admin/AdminProductbeheer.cshtml");
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