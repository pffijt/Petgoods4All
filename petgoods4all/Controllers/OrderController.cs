﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using petgoods4all.Models;
using petgoods4all.Controllers;
using PayPal.Core;
using PayPal.v1;

namespace petgoods4all.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewOrder(int orderId)
        {
            var db = new ModelContext();
            var result = from s in db.OrderedProducts where s.OrderId == orderId select s;

            List<Voorraad> voorraadList = new List<Voorraad>();

            foreach (var item in result)
            {
                var orderedProductsResult = (from s in db.Voorraad where s.Id == item.ProductId select s).Single();

                voorraadList.Add(new Voorraad {
                    Id = orderedProductsResult.Id,
                    Naam = orderedProductsResult.Naam,
                    Dier = orderedProductsResult.Dier,
                    Subklasse = orderedProductsResult.Subklasse,
                    Kwantiteit = item.Quantity,
                    Prijs = orderedProductsResult.Prijs,
                    image = orderedProductsResult.image,
                });
            
            }

            ViewBag.ViewOrder = voorraadList;

            return View();
        }

        public ActionResult OrderHistory()
        {
            var db = new ModelContext();

            var UserId = HttpContext.Session.GetInt32("UID");
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
        public ActionResult CustOrder(string prijs)
        {
            @ViewBag.Prijs = prijs;

            var db = new ModelContext();
            var UserId = HttpContext.Session.GetInt32("UID");
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

            return View();
        }

        [HttpPost]
        public ActionResult OrderProducts(string prijs)
        {
            var db = new ModelContext();

            var UserId = HttpContext.Session.GetInt32("UID");
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

                var voorraad = (from v in db.Voorraad where v.Id == item.VoorraadId select v).Single();
                if (voorraad.Kwantiteit < 0)
                {
                    ViewBag.Error = "NietInVoorraad";
                    return View("~/Views/Order/OrderError.cshtml");
                }
                voorraad.Kwantiteit -= item.Quantity;
                db.SaveChanges();

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
        [HttpPost]
        public async Task<ActionResult> Pay(string prijs)
        {
            var db = new ModelContext();
            var UserId = HttpContext.Session.GetInt32("UID");
            int UserIdResult = (from s in db.Account where s.id == UserId select s.id).Single();
            string o_name =  (from s in db.Account where s.id == UserIdResult select s.achternaam).Single();
            string o_postal = (from s in db.Account where s.id == UserIdResult select s.postcode).Single();
            string o_address = (from s in db.Account where s.id == UserIdResult select s.straatnaam).Single();
            string o_number = (from s in db.Account where s.id == UserIdResult select s.huisnummer).Single();
            // string o_aanhef, string o_name, string o_postal, string o_address, string o_number
            var environment = new SandboxEnvironment("ATAmdaFGY2Pz6CH83fmdK8OaXu2Wd8b9fLDyuU8X3SNiAzvu2_Ks4IU3wPiNbpE74nWIkhb4jN_7pz9E", "EOksjziNOaGEYh-OroCWTFT_EKDlqJEIpsrZLMtUhmYNxgDZ_v6KGwyL1MFcWJ-dfv97PApRKroAAT0g");
            var Pay_client = new PayPalHttpClient(environment);
            
            var payment = new PayPal.v1.Payments.Payment()
            {
                Intent = "sale",
                Transactions = new List<PayPal.v1.Payments.Transaction>() 
                {
                    new PayPal.v1.Payments.Transaction()
                    {
                        Amount = new PayPal.v1.Payments.Amount()
                        {
                            Total = prijs,
                            Currency = "EUR",
                            Details = new  PayPal.v1.Payments.AmountDetails() 
                            {
                                Subtotal = prijs
                            }
                        },
                        ItemList = new PayPal.v1.Payments.ItemList()
                            {
                            Items = new List<PayPal.v1.Payments.Item>()
                            {
                                new PayPal.v1.Payments.Item()
                                {
                                    Name="Petgoods4All Products",
                                    Currency = "EUR",
                                    Price = prijs,
                                    Quantity = "1",
                                    Description = "Bedankt voor uw aankoop"
                                }   
                            }
                        }
                        ,
                        Description="Betaling Petgoods4All"
                    }
                },
                RedirectUrls = new PayPal.v1.Payments.RedirectUrls() 
                {
                    CancelUrl = "http://localhost:56003/",
                    ReturnUrl = "http://localhost:56003/Order/OrderProducts?prijs="+prijs+"&o_aanhef="+"DHR/MVR."+"&o_name="+o_name+"&o_postal="+o_postal+"&o_address="+o_address+"&o_number="+o_number
                },
                Payer = new PayPal.v1.Payments.Payer() 
                {
                    PaymentMethod = "paypal"
                }
            };
            PayPal.v1.Payments.PaymentCreateRequest request = new PayPal.v1.Payments.PaymentCreateRequest();
            request.RequestBody(payment);
            System.Net.HttpStatusCode statusCode;

            try 
            {

                BraintreeHttp.HttpResponse httpResponse = await Pay_client.Execute(request);
                BraintreeHttp.HttpResponse response = httpResponse;
                statusCode = response.StatusCode;
                PayPal.v1.Payments.Payment result = response.Result<PayPal.v1.Payments.Payment>();
                string redirectUrl = null;
                    foreach(PayPal.v1.Payments.LinkDescriptionObject link in result.Links)
                    {
                        if(link.Rel.Equals("approval_url"))
                        {
                            redirectUrl = link.Href;
                            Console.WriteLine(redirectUrl);
                            return Redirect(redirectUrl);
                        }
                    }
            } 
            catch(BraintreeHttp.HttpException httpException) 
            {
                statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                Console.WriteLine(debugId);
                return CustOrder(prijs);
            }
            return CustOrder(prijs);

        }
        public async Task<ActionResult> OrderProducts()
        {
            var o_aanhef= HttpContext.Request.Query["o_aanhef"].ToString();
            var o_name= HttpContext.Request.Query["o_name"].ToString();
            var o_postal= HttpContext.Request.Query["o_postal"].ToString();
            var o_address= HttpContext.Request.Query["o_address"].ToString();
            var o_number= HttpContext.Request.Query["o_number"].ToString();
            var prijs= HttpContext.Request.Query["prijs"].ToString();
            var paymentId = HttpContext.Request.Query["paymentId"].ToString();
            var PayerID = HttpContext.Request.Query["PayerID"].ToString();
            var environment = new SandboxEnvironment("ATAmdaFGY2Pz6CH83fmdK8OaXu2Wd8b9fLDyuU8X3SNiAzvu2_Ks4IU3wPiNbpE74nWIkhb4jN_7pz9E", "EOksjziNOaGEYh-OroCWTFT_EKDlqJEIpsrZLMtUhmYNxgDZ_v6KGwyL1MFcWJ-dfv97PApRKroAAT0g");
            var Pay_client = new PayPalHttpClient(environment);

            PayPal.v1.Payments.PaymentExecuteRequest request = new PayPal.v1.Payments.PaymentExecuteRequest(paymentId);
            request.RequestBody(new PayPal.v1.Payments.PaymentExecution()
            {
                PayerId = PayerID
            });
            BraintreeHttp.HttpResponse response = await Pay_client.Execute(request);
            Console.WriteLine("Shiet man");

            if(o_aanhef == null)
            {
                o_aanhef = "";
            }
            var db = new ModelContext();
            var UserId = HttpContext.Session.GetInt32("UID");
            int UserIdResult = (from s in db.Account where s.id == UserId select s.id).Single();

            var ShoppingCartResult = from s in db.ShoppingCart where s.AccountId == UserId select s;
            Console.WriteLine(o_name+o_postal+o_address+o_number);
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
            //Bestellingsmail voor Petgoods4All
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("petgoods4all@gmail.com");
            mail.To.Add("petgoods4all@gmail.com");
            mail.Subject = "Order: "+ MaxId + 1;
            mail.Body = "Order placed from user "+UserId+".\n";
            foreach(var item in check)
            {
                var item2 = (from s in db.Voorraad where item.VoorraadId == s.Id select s.Naam).Single();
                mail.Body = mail.Body + "Product: "+item2+"\n";
            }
            mail.Body = mail.Body + "aanhef: "+o_aanhef+"\n"+"adres: "+o_address+"\n"+"postcode: "+o_postal+"\n"+"huisnummer: "+o_number+"\n"
            +"naam: "+o_name+"\n"; 
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("petgoods4all@gmail.com", "adminpetgoods4all");
            client.EnableSsl = true;
            client.Send(mail);
            Console.WriteLine("Mail sent to Petgoods4all");
            // Einde Bestellingsmail voor Petgoods4All
            //Bevestigingsmail besteller
            string UserEmail = (from s in db.Account where s.id == UserId select s.email).Single();
            mail.To.Clear();
            mail.To.Add(UserEmail);
            mail.Subject = "Order: "+ MaxId + 1;
            mail.Body = "Geachte "+o_aanhef+" "+o_name+", \n"
            +"Bedankt voor uw bestelling.\n"+
            "De producten komen uw kant op.\n"+
            "adres:"+o_address+"\n"+
            "postcode: "+o_postal+"\n"+
            "Veel dierenplezier met de producten!\n Met vriendelijke groeten,\n\n Petgoods4All";
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("petgoods4all@gmail.com", "adminpetgoods4all");
            client.EnableSsl = true;
            client.Send(mail);
            Console.WriteLine("KlantMail sent ");
            return OrderHistory();
        }
    }
}