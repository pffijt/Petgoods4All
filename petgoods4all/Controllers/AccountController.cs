using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;
using petgoods4all.Models;

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
            var email = inputEmail;
            var password = inputPassword;
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
                password = confirmPassword
            };

            db.Account.Add(a);
            db.SaveChanges();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("petgoods4all@gmail.com", "adminpetgoods4all");

            MailMessage mm = new MailMessage("petgoods4all@gmail.com", email, "test", "test");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);

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
                HttpContext.Session.SetString("resultEmail", resultEmail);

                if (resultAdmin == true)
                {
                    string strEmailId = HttpContext.Session.GetString("resultEmail");
                    return View("~/Views/Home/About.cshtml");
                }
                else
                {
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            else
            {
                return View("~/Views/Account/Inloggen.cshtml");
            }
        }
    }
}