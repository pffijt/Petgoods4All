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
        public ActionResult Aanmelden(string inputEmail, string inputPassword, string confirmPassword, string inputVoornaam, string inputAchternaam, string inputStraatnaam, string inputTelefoonnummer)
        {
            var email = inputEmail;
            var password = inputPassword;
            var voornaam = inputVoornaam;
            var achternaam = inputAchternaam;
            var straatnaam = inputStraatnaam;
            var telefoonnummer = inputTelefoonnummer; 
            int MaxId;

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
                telefoonnummer = inputTelefoonnummer

            };

            db.Account.Add(a);
            db.SaveChanges();

            MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "petgoods4all@gmail.com";
            string emailPassword = "adminpetgoods4all";
            message.From = new MailAddress(fromEmail);
            message.To.Add(email);
            message.Subject = "Activatie mail";
            message.Body = @"Klik op de link om uw account te activeren http://localhost:56002/Account/Inloggen";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, emailPassword);

                smtpClient.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
            }

            return View("~/Views/Account/Inloggen.cshtml");
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

            if (resultEmail == email && resultPassword == password)
            {
                //HttpContext.Session.SetString("resultEmail", resultEmail);

                if (resultAdmin == true)
                {
                    //string strEmailId = HttpContext.Session.GetString("resultEmail");
                    return View("~/Views/Admin/AdminHome.cshtml");
                }
                else
                {
                    return View("~/Views/User/UserHome.cshtml");
                }
            }
            else
            {
                return View("~/Views/Account/Inloggen.cshtml");
            }
        }
    }
}