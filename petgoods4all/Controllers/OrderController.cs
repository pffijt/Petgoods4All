﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using petgoods4all.Models;
using petgoods4all.Controllers;

namespace petgoods4all.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderHistory()
        {
            var db = new ModelContext();

            var UserId = HttpContext.Session.GetInt32("UserIdKey");
            if (UserId == null)
            {
                return View("~/Views/Account/Inloggen.cshtml");
            }
            else
            {
                int UserIdResult = (from s in db.Account where s.id == UserId select s.id).Single();
            }

            var OrderResult = from s in db.Order where s.AccountId == UserId select s;

            ViewBag.Orders = OrderResult;

            return View("~/Views/Order/OrderHistory.cshtml");
        }

        [HttpPost]
        public ActionResult OrderProducts(string prijs)
        {
            var db = new ModelContext();

            var UserId = HttpContext.Session.GetInt32("UserIdKey");
            int UserIdResult = (from s in db.Account where s.id == UserId select s.id).Single();

            var ShoppingCartResult = from s in db.ShoppingCart where s.AccountId == UserId select s;

            //MaxId
            int MaxId = 0;
            var result = from s in db.Order select s.Id;
            if (!result.Any())
            {
            }
            else
            {
                MaxId = result.Max();
            }


            Order order = new Order
            {
                Id = MaxId + 1,
                AccountId = UserIdResult,
                Datum = DateTime.Now,
                Prijs = prijs,
                OrderStatus = "Pending",
            };

            db.Order.Add(order);
            db.SaveChanges();

            foreach (var item in ShoppingCartResult)
            {
                int MaxOrderedProductsId = 0;
                var resultt = from s in db.OrderedProducts select s.Id;
                if (!resultt.Any())
                {
                }
                else
                {
                    MaxOrderedProductsId = resultt.Max();
                }

                OrderedProducts orderedProducts = new OrderedProducts
                {
                    Id = MaxOrderedProductsId + 1,
                    OrderId = MaxId + 1,
                    ProductId = item.VoorraadId,
                    Quantity = item.Quantity,
                };

                db.OrderedProducts.Add(orderedProducts);
                db.SaveChanges();

            }
            var check = (from s in db.ShoppingCart where s.AccountId == UserId select s).ToList();
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("petgoods4all@gmail.com");
            mail.To.Add("petgoods4all@gmail.com");
            mail.Subject = "Order: "+ MaxId + 1;
            mail.Body = "Order placed from user "+UserId+".<br/>";
            foreach(var item in check)
            {
                var item2 = (from s in db.Voorraad where item.VoorraadId == s.Id select s.Naam).Single();
                mail.Body = mail.Body + "Product: "+item2+"<br/>";
            }
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("petgoods4all@gmail.com", "adminpetgoods4all");
            client.EnableSsl = true;
            client.Send(mail);
            Console.WriteLine("Mail sent");
            
            return OrderHistory();
        }
    }
}