using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;
using petgoods4all.Models;
using System.Net;

namespace petgoods4all.Controllers
{
    public class AccountController : Controller
    {
        ModelContext db = new ModelContext();
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
        public ActionResult Uitloggen()
        {
         
            return View();
        }

        [HttpPost]
        public JsonResult CheckEmail(string inputEmail){
            var db = new ModelContext();
            System.Threading.Thread.Sleep(200);
            var SeachData = db.Account.Where( x => x.email == inputEmail).SingleOrDefault();
            if(SeachData != null)
            {
                return Json(1);
            }
            else{
                return Json(0);
            }
        }

        [HttpPost]
        public ActionResult Aanmelden(string inputEmail, string inputPassword, string inputHuisnummer,string inputPostcode, string inputProvincie, string confirmPassword, string inputVoornaam, string inputAchternaam, string inputStraatnaam, string inputTelefoonnummer)
        {
            int MaxId;
            var email = inputEmail;
            var password = inputPassword;
            var voornaam = inputVoornaam;
            var achternaam = inputAchternaam;
            var straatnaam = inputStraatnaam;
            var telefoonnummer = inputTelefoonnummer;
            var huisnummer = inputHuisnummer;
            var postcode = inputPostcode;
            var provincie = inputProvincie; 

            var db = new ModelContext();
            var result = from acc in db.Account select acc.id;

            if (!result.Any())
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
                password = confirmPassword,
                voornaam = inputVoornaam,
                achternaam = inputAchternaam,
                straatnaam = inputStraatnaam,
                huisnummer = inputHuisnummer,
                postcode = inputPostcode,
                provincie = inputProvincie,
                telefoonnummer = inputTelefoonnummer,
                IsEmailVerified =  false,
            };

            MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "petgoods4all@gmail.com";
            string emailPassword = "adminpetgoods4all";
            message.From = new MailAddress(fromEmail);
            message.To.Add(email);
            message.Subject = "Activatie mail";
            message.Body = @"Klik op de link om uw account te activeren http://localhost:56003/Account/Verification?uid=" + (MaxId + 1);
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, emailPassword);

                smtpClient.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
            }

            db.Account.Add(a);
            db.SaveChanges();
            return Redirect("Inloggen");
        }

        public ActionResult Verification(string uid)
        {
            var db = new ModelContext();
            int a = Int32.Parse(uid);
            var result = (from acc in db.Account where a == acc.id select acc.IsEmailVerified).Single();
            
            if (result == false)
            {
                (from acc in db.Account where a == acc.id select acc).ToList().ForEach(acc => acc.IsEmailVerified = true);
            }
            db.SaveChanges();
            return Redirect("http://localhost:56003");
        }

        [HttpPost]
        public ActionResult Inloggen(string inputEmail, string inputPassword)
        {
            var email = inputEmail;
            var password = inputPassword;

            var db = new ModelContext();

            var resultEmail = (from acc in db.Account where email == acc.email select acc.email).Single();
            var resultPassword = (from acc in db.Account where password == acc.password select acc.password).Single();
            var resultAdmin = (from acc in db.Account where email == acc.email select acc.Admin).Single();
            var resultId = (from acc in db.Account where email == acc.email select acc.id).Single();
            var resultVerified = (from acc in db.Account where email == acc.email select acc.IsEmailVerified).Single();

            if (resultEmail == email && resultPassword == password && resultVerified == true)
            {
                //HttpContext.Session.SetString("resultEmail", resultEmail);
                HttpContext.Session.SetInt32(("UID"),resultId);

                if (resultAdmin == true)
                {
                    //string strEmailId = HttpContext.Session.GetString("resultEmail");
                    return RedirectToAction("AdminHome", "Admin");
                }
                else
                {
                    return RedirectToAction("UserHome", "User");
                }
            }
            else
            {
                return Redirect("Inloggen");
            }
        }
    }
}