using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
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

        
        public ActionResult Klantpersonalize()
        {
            var userId = HttpContext.Session.GetInt32("UID");
            if(userId == null)
            {
                return Redirect("http://localhost:56003/");
            }
            using(db)
            {
                var checkUID = (from a in db.Account where userId == a.id select a.id).Single();
                var checkPA = (from a in db.Personal_animal where checkUID == a.user_id select a).Any();
                if(checkPA == false)
                {
                    ViewBag.empty = true;
                }
                else
                {
                    ViewBag.empty = true;
                    //ViewBag.empty = false;
                }
                ViewBag.hond = (from a in db.Personal_animal where userId == a.user_id && "Dog" == a.animal select a).Any();
                ViewBag.reptiel = (from a in db.Personal_animal where userId == a.user_id && "Reptile" == a.animal select a).Any();
                ViewBag.vis = (from a in db.Personal_animal where userId == a.user_id && "Fish" == a.animal select a).Any();
                ViewBag.kat = (from a in db.Personal_animal where userId == a.user_id && "Cat" == a.animal select a).Any();
            }
            return View();
        }
        public EmptyResult Addpersonal(string a)
        {
            int MaxId;
            var userId = HttpContext.Session.GetInt32("UID").GetValueOrDefault(0);
                var checkAnimal = (from s in db.Personal_animal where userId == s.user_id && a == s.animal select s).Any();
                if(checkAnimal == false)
                {
                    var result = from acc in db.Personal_animal select acc.id;
                    if (!result.Any())
                    {
                        MaxId = 0;
                    }
                    else
                    {
                        MaxId = result.Max();
                    }
                    Personal_animal b = new Personal_animal
                    {
                        id = MaxId + 1,
                        user_id = userId,
                        animal = a,
                    };
                    db.Personal_animal.Add(b);
                }
           
            return new EmptyResult();
        }
        public EmptyResult Removepersonal(string a)
        {
            var userId = HttpContext.Session.GetInt32("UID").GetValueOrDefault(0);
        
                var checkAnimal = (from s in db.Personal_animal where a == s.animal && userId == s.user_id select s).Any();
                if(checkAnimal == true)
                {
                    var findID = (from acc in db.Personal_animal where acc.animal == a && userId == acc.user_id select acc.id).Single();
                    Personal_animal b = new Personal_animal
                    {
                        id = findID,
                        user_id = userId,
                        animal = a,
                    };
                    db.Personal_animal.Remove(b);
                }
            
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult Klantpersonalize(bool personCat = false, bool personDog = false, bool personFish = false, bool personReptile = false)
        {
            if(personCat == true)
            {
                Addpersonal("Cat");
                db.SaveChanges();
            }
            if(personCat == false)
            {
                Removepersonal("Cat");
                db.SaveChanges();
            }

            if(personDog == true)
            {
                Addpersonal("Dog");
                db.SaveChanges();
            }
            if(personDog == false)
            {
                Removepersonal("Dog");
                db.SaveChanges();
            }

            if(personFish == true)
            {
                Addpersonal("Fish");
                db.SaveChanges();
            }
            if(personFish == false)
            {
                Removepersonal("Fish");
                db.SaveChanges();
            }

            if(personReptile == true)
            {
                Addpersonal("Reptile");
                db.SaveChanges();
            }
            if(personReptile == false)
            {
                Removepersonal("Reptile");
                db.SaveChanges();
            }

            return Redirect("~/User/Klantpersonalize");
        }
        public ActionResult UserBeheer()
        {
             // var UserId = HttpContext.Session.GetInt32("UID");
            var userId = HttpContext.Session.GetInt32("UID");
            ViewBag.userId = userId;

            using (db)
            {

                var model = from acc in db.Account select acc;
                // var model = from acc in db.Account
                //             select new Account
                //     {
                //         voornaam = acc.voornaam,
                //         achternaam = acc.achternaam,
                //         telefoonnummer = acc.telefoonnummer,
                //         straatnaam = acc.straatnaam,
                //         password = acc.password,
                //         email = acc.email
                //     };
                ViewBag.AccountData = model.ToList();

            }
            return View();
        }
        
        //
        public ActionResult ReadAccount(string inputEmailUser)
        {
            Account account = db.Account.Find(inputEmailUser);

            return View(account);
        }

        public ActionResult updateGegevens()
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                Account account = db.Account.FirstOrDefault(x => x.id == UserID);

                return View(account);
            }
        }


        public ActionResult UpdateAccountSave(string inputEmailUser, string inputProvincieUser, string inputHuisnummerUser, string inputPostcodeUser, string originalEmailUser, string inputPasswordUser, string inputVoornaamUser, string inputAchternaamUser, string inputTelefoonnummerUser, string inputStraatnaamUser)
        {            
            using (db)
            {
                var userId = HttpContext.Session.GetInt32("UID");
                
                //var accountUpdate = db.Account.Find(inputEmailUser);// input from inlog, comparing db email.
                var accountUpdate = db.Account.Where(account => account.id == userId).FirstOrDefault();
                accountUpdate.voornaam = inputVoornaamUser;
                accountUpdate.achternaam = inputAchternaamUser;
                accountUpdate.telefoonnummer = inputTelefoonnummerUser;
                accountUpdate.straatnaam = inputStraatnaamUser;
                accountUpdate.password = inputPasswordUser;
                accountUpdate.postcode = inputPostcodeUser;
                accountUpdate.provincie = inputProvincieUser;
                accountUpdate.huisnummer = inputHuisnummerUser;
                accountUpdate.email = inputEmailUser;

                db.Account.Update(accountUpdate);
                db.SaveChanges();
            }
            return RedirectToAction("UserHome");
        }

        public ActionResult readAccount()
        {
            using (db)
            {

                var model = from acc in db.Account select acc;
                // var model = from acc in db.Account
                //             select new Account
                //     {
                //         voornaam = acc.voornaam,
                //         achternaam = acc.achternaam,
                //         telefoonnummer = acc.telefoonnummer,
                //         straatnaam = acc.straatnaam,
                //         password = acc.password,
                //         email = acc.email
                //     };
                ViewBag.AccountData = model.ToList();

                return View();
            }
        }

        public ActionResult DeleteAccount(string inputEmailUser)
        {
            Account account = db.Account.Find(inputEmailUser);
            db.Account.Remove(account);

            return View();
        }        
    }
}