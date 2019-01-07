using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using petgoods4all.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Globalization;

namespace petgoods4all.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var culture = new System.Globalization.CultureInfo("nl-NL");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            Console.WriteLine("Cultureinfo : "+CultureInfo.CurrentCulture);
            var UID = HttpContext.Session.GetInt32("UID");
            List<string> lista = new List<string>();
            using (var db = new ModelContext())
            {
                if(UID != null)
                {
                    var checkAssignedPers = (from a in db.Personal_animal where UID == a.user_id select a).Any();
                    if(checkAssignedPers == true)
                    {
                        
                        var checkDog = (from a in db.Personal_animal where UID == a.user_id && "Dog" == a.animal select a).Any();
                        var checkCat = (from a in db.Personal_animal where UID == a.user_id && "Cat" == a.animal select a).Any();
                        var checkFish = (from a in db.Personal_animal where UID == a.user_id && "Fish" == a.animal select a).Any();
                        var checkReptile = (from a in db.Personal_animal where UID == a.user_id && "Reptile" == a.animal select a).Any();
                        if(checkDog)
                        {
                            lista.AddRange(new List<string> {"dog_1.jpg","dog_2.jpg","dog_3.jpg","dog_4.jpg","dog_5.jpg"});
                        }
                        if(checkCat)
                        {
                             lista.AddRange(new List<string> {"cat_1.jpg","cat_2.jpg","cat_3.jpg","cat_4.jpg","cat_5.jpg"});
                        }
                        if(checkFish)
                        {
                             lista.AddRange(new List<string> {"fish_1.jpg","fish_2.jpg","fish_3.jpg","fish_4.jpg","fish_5.jpg"});
                        }
                        if(checkReptile)
                        {
                             lista.AddRange(new List<string> {"reptile_1.jpg","reptile_2.jpg","reptile_3.jpg","reptile_4.jpg","reptile_5.jpg"});
                        }
                        var newList = lista.OrderBy(a => Guid.NewGuid()).Take(5).ToList();
                        ViewBag.x = newList[0];
                        ViewBag.y = newList[1];
                        ViewBag.z = newList[2];
                        ViewBag.g = newList[3];
                        ViewBag.h = newList[4];
                        var checkName = (from a in db.Account where UID == a.id select a.voornaam).Single();
                        ViewBag.name = checkName;
                    }

                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(){
            return View();
        }

        public ActionResult AddToWishpage()
        {
            return View();
        }

        public List<int> Related(int indentication)
        {
            List<int> productsID = new List<int>();
            using (var db = new ModelContext())
            {
                string getType = (from m in db.Voorraad where m.Id == indentication select m.Dier).Single();
                string getSub = (from m in db.Voorraad where m.Id == indentication select m.Subklasse).Single();
                var getRelated = (from m in db.Voorraad where m.Dier == getType && m.Subklasse == getSub && m.Id != indentication select m.Id).ToList();

                if (getRelated.Count() < 3 || getRelated == null)
                {
                    getRelated = (from m in db.Voorraad where m.Dier == getType && m.Id != indentication select m.Id).ToList();
                }
                productsID = ((getRelated.OrderBy(x => Guid.NewGuid())).Take(3).ToList());

            }
            return productsID;
        }

        public class AccountReview {
            public int Id { get; set; }
            public string Description { get; set; }
            public int StarRating { get; set; }
            public int UserId { get; set; }
            public int ProductId { get; set; }
            public string Naam { get; set; }
            public string Achternaam { get; set; }
            public string Email { get; set; }
        }

        public ActionResult Productpage(int identication = 1)
        {
            ViewBag.Message = "Product page";
            ViewBag.ProductId = identication;
            var db = new ModelContext();
            var product = from m in db.Voorraad where m.Id == identication select m;
            var UID = HttpContext.Session.GetInt32("UID");
            ViewBag.UID = UID;
            ViewBag.hoeveelheid = (from m in db.Voorraad where identication == m.Id select m.Kwantiteit).Single();
            var reviews =
                from r in db.Review
                join u in db.Account on r.UserId equals u.id
                orderby r.Id descending
                select new AccountReview
                {
                    Id = r.Id,
                    Description = r.Description,
                    StarRating = r.StarRating,
                    UserId = r.UserId,
                    ProductId = r.ProductId,
                    Naam = u.voornaam,
                    Achternaam = u.achternaam,
                    Email = u.email
                };

            var averageRating = from r in db.Review where r.ProductId == identication select r.StarRating;

            var reviewList = reviews.ToList();
            ViewBag.reviews = reviewList;
            ViewBag.UserId = UID;
            ViewBag.Model = product.ToList();

            List<int> relatedProducts = Related(identication);
            foreach (var item in relatedProducts)
            {
                Console.WriteLine(item.ToString());
            }

            petgoods4all.Models.Voorraad products = (from m in db.Voorraad where m.Id == identication select m).Single();
            List<petgoods4all.Models.Voorraad> otherProducts = (from m in db.Voorraad where m.Id == relatedProducts[0]
            || m.Id == relatedProducts[1] || m.Id == relatedProducts[2] select m).ToList();
            otherProducts.Add(products);



            if (!averageRating.Any())
            {
                //De hoofdproduct en 3 gerelateerde
                ViewBag.AverageRating = 0;
            }
            else
            {
                var average = (from r in db.Review where r.ProductId == identication select r.StarRating).Average();
                ViewBag.AverageRating = average;
            }

            return View(otherProducts);
        }

        [HttpPost]
        public ActionResult Productpage()
        {
            int identication = Int32.Parse(HttpContext.Request.Query["identication"].ToString());
            using (var db = new ModelContext())
            {
                int pID = (from m in db.Voorraad where m.Id == identication select m.Id).Single();
                int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
                var UID = HttpContext.Session.GetInt32("UID");
                ViewBag.UID = uID;
  
                var product = from m in db.Voorraad where m.Id == identication select m;
                //var reviews = from r in db.Review where r.ProductId == identication select r;
                
                var reviews =
                    from r in db.Review
                    join u in db.Account on r.UserId equals u.id
                    select new
                    {
                        Id = r.Id,
                        Description = r.Description,
                        StarRating = r.StarRating,
                        UserId = r.UserId,
                        ProductId = r.ProductId,
                        Naam = u.voornaam,
                        Achternaam = u.achternaam,
                        Email = u.email
                    };
                var reviewList = reviews.ToList();
                ViewBag.reviews = reviewList;
                ViewBag.UserId = UID;

                //List<string> users = new List<string>();

                //foreach (var item in reviews)
                //{
                //    var user = (from u in db.Account where u.id == item.UserId select u.voornaam).Single();
                //    users.Add(user);
                //}

                return View(product.ToList());
            }
        }
        [HttpPost]
       public ActionResult Contact(string contactEmail, string Onderwerp, string Bericht, string button)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(contactEmail));
            message.To.Add(new MailboxAddress("petgoods4all@gmail.com"));
            message.Subject = Onderwerp;
            message.Body = new TextPart("html")
            {
                Text = "From: " + contactEmail + "<br>" +
                "Onderwerp: " + Onderwerp + "<br>" +
                "Bericht: " + Bericht
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate("petgoods4all@gmail.com", "adminpetgoods4all");
                client.Send(message);
                client.Disconnect(false);
            }

            if (button == "btnSubmit"){
                TempData["buttonVal"] = "Uw bericht is verstuurd!";
            }
            else{
                TempData["buttonVal"] = "Uw bericht is niet verstuurd! Probeer het later nog eens";    
            }
           
            return RedirectToAction("Contact");
        }

        [HttpPost]
        public ActionResult addWishList(int Quantity = 1)
        {
            int identication = Int32.Parse(HttpContext.Request.Query["identication"].ToString());
            using (var db = new ModelContext())
            {
                int pID = (from m in db.Voorraad where m.Id == identication select m.Id).Single();
                int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
                var UID = HttpContext.Session.GetInt32("UID");
                if (uID != 0)
                {
                    int MaxId;
                    var result = from m in db.Wishlist select m.id;
                    if (!result.Any())
                    {
                        MaxId = 0;
                    }
                    else
                    {
                        MaxId = result.Max();
                    }
                    Wishlist n = new Wishlist
                    {
                        id = MaxId + 1,
                        customerid = uID,
                        productid = pID,
                        quantity = Quantity
                    };
                    db.Wishlist.Add(n);
                    db.SaveChanges();
                }
                var product = from m in db.Voorraad where m.Id == identication select m;
                return Redirect("AddToWishpage");
            }
        }
                public ActionResult AddReviews(string description, int productId, int rating)
                {
                    ViewBag.Message = "Product page";
                    var db = new ModelContext();
                    var MaxId = 0;
                    var UID = HttpContext.Session.GetInt32("UID");
                    var UserId = UID.GetValueOrDefault();
                    //var ProductPage = new HomeController().Productpage(productId);

                    var result = from s in db.Review select s.Id;
                    if (!result.Any())
                    {
                    }
                    else
                    {
                        MaxId = result.Max();
                    }

                    Review review = new Review
                    {
                        Id = MaxId + 1,
                        Description = description,
                        StarRating = rating,
                        ProductId = productId,
                        UserId = UserId,
                    };

                    db.Review.Add(review);
                    db.SaveChanges();

                    return Redirect("/Home/productpage?identication=" + productId);
                }

        public ActionResult Wishpage()
        {
            int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
            ViewBag.Message = "This is your wishpage.";
            ViewBag.uID = uID;
            if(uID != 0)
            {
                using(var db = new ModelContext())
                {
                    var _emptylist = (from m in db.Wishlist where m.customerid == uID  select m).ToList();
                    if(_emptylist.Any())
                    {
                        Console.WriteLine("List is not empty");
                        ViewBag.emptylist = false;
                        List<petgoods4all.Models.Voorraad> pList = new List<petgoods4all.Models.Voorraad>();
                        List<int> intList = new List<int>();
                        int index = 1;
                        int MaxId;
                        var myList = (from m in db.Wishlist where m.customerid == uID select m).ToList();
                        if( !myList.Any() )
                        {
                            MaxId = 0;
                        }
                        else
                        {
                            MaxId = myList.Count();
                            while(index != MaxId+1)
                            {
                                var pID = (from s in myList select s.productid).Skip(index-1).Take(1).Single();
                                var p = (from m in db.Voorraad where pID == m.Id select m).Single();
                                pList.Add(p);
                                var number = (from a in db.Wishlist where uID == a.customerid && pID == a.productid select a.quantity).Single();
                                intList.Add(number);
                                index = index + 1;
                            }
                            ViewBag.quantityList = intList;
                        }
                        var _PIDD = HttpContext.Session.GetInt32("PIDD");
                        if(_PIDD != null)
                        {
                            ViewBag.productAdded = (from s in db.Voorraad where _PIDD == s.Id select s.Naam).Single();
                            HttpContext.Session.Remove("PIDD");
                        }

                        return View(pList);
                    }
                    else
                    {
                        ViewBag.emptylist = true;
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Wishpage(string Option, int pIDD, int hoeveelheid)
        {
            //Dit is wel echt slecht gescriptmaarja ik weet ook niet anders
            if(Option == "+ Naar winkelwagen")
            {
                int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
                Console.WriteLine("uID:"+uID);
                if(uID != 0)
                {
                    var db = new ModelContext();
                    var MaxId = 0;
                    HttpContext.Session.SetInt32("PIDD", pIDD);
                    List<int> productList = new List<int>();
                    productList.Add(pIDD);
                    int AccountId = 0;
                    var UserId = HttpContext.Session.GetInt32("UID");
                    if (UserId == null)
                    {
                        AccountId = 0;
                    }
                    else
                    {
                        AccountId = UserId.Value;
                    }
                    var result = from s in db.ShoppingCart select s.Id;
                    if (!result.Any())
                    {
                    }
                    else
                    {
                        MaxId = result.Max();
                    }
                    if (AccountId == 0)
                    {
                        return View("~/Views/Account/Inloggen.cshtml");
                    }
                    else
                    {
                        var checkAlreadyIn = (from s in db.ShoppingCart where pIDD == s.VoorraadId && uID == s.AccountId select s).Any();
                        if(checkAlreadyIn)
                        {
                            var MogelijkGetal = (from a in db.Voorraad where pIDD == a.Id select a.Kwantiteit).Single();
                            var eerstGetal = (from b in db.ShoppingCart where pIDD == b.VoorraadId && uID == b.AccountId select b.Quantity).Single();
                            if(eerstGetal+hoeveelheid <= MogelijkGetal)
                            {
                                (from s in db.ShoppingCart where pIDD == s.VoorraadId && uID == s.AccountId select s)
                                .ToList().ForEach(s => s.Quantity = s.Quantity+hoeveelheid);
                            }
                        }
                        else
                        {
                            ShoppingCart shoppingCart = new ShoppingCart
                            {
                                Id = MaxId + 1,
                                VoorraadId = pIDD,
                                AccountId = AccountId,
                                Quantity = hoeveelheid,
                            };
                            db.ShoppingCart.Add(shoppingCart);
                        }
                        db.SaveChanges();
                    }
                    return Redirect("Wishpage");
                }
            }
            if(Option == "x Verwijderen")
            {
                int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
                Console.WriteLine("uID:"+uID);
                if(uID != 0)
                {
                    using (var db = new ModelContext())
                    {
                        List<petgoods4all.Models.Wishlist> wList = new List<petgoods4all.Models.Wishlist>();
                        var currentstate = from s in db.Wishlist select s;
                        var currentList = currentstate.ToList();
                        db.Wishlist.RemoveRange(currentstate);
                        db.SaveChanges();
                        int index = 1;
                        int skipped = 0;
                        int MaxId = currentList.Count();
                        Console.WriteLine("MaxId:"+MaxId);
                        while(index != MaxId+1)
                        {
                            var currentrow = (from s in currentList select s).Skip(index-1).Take(1).Single();
                            if(currentrow.productid != pIDD)
                            {
                                int ind = index - skipped;
                                currentrow.id = ind;
                                wList.Add(currentrow);
                                index = index +1;
                            }
                            if(currentrow.productid == pIDD && currentrow.customerid == uID)
                            {
                                skipped = 1;
                                index = index +1;
                            }
                        }
                        foreach(var item in wList)
                        {
                            db.Wishlist.Add(item);
                        }
                        db.SaveChanges();
                        return Redirect("Wishpage");
                    }
                }

            }
            return View();
        }
        public IActionResult ProductBrowsen(string D, string sel2, int P = 1)
        {
            ViewBag.Message = "Producten";
            using (var db = new ModelContext())
            {
                System.Collections.Generic.List<petgoods4all.Models.Voorraad> goodss = new System.Collections.Generic.List<petgoods4all.Models.Voorraad>();
                //pagination per soort
                ViewBag.firstnum = (P*16)-15;
                ViewBag.secondnum = 16*P;
                ViewBag.paginationindex = P;
                ViewBag.animal = D;
                var goods = from m in db.Voorraad where m.Dier == D select m;
                ViewBag.count = goods.Count();
                if(ViewBag.secondnum > ViewBag.count)
                {
                    ViewBag.secondnum = ViewBag.count;
                }
                if(sel2 != null || sel2 == " ")
                {
                    if(sel2 == "Prijs ophogend")
                    {
                        goodss = goods.OrderBy(o=>double.Parse(o.Prijs)).ToList();
                        ViewBag.sel2 = sel2;
                    }
                    if(sel2 == "Prijs verlagend")
                    {
                        goodss = goods.OrderByDescending(o=>double.Parse(o.Prijs)).ToList();
                        ViewBag.sel2 = sel2;
                    }
                    if(sel2 == "Alfabetisch A-Z")
                    {
                        goodss = goods.OrderBy(o=>o.Naam).ToList();
                        ViewBag.sel2 = sel2;
                    }
                    if(sel2 == "Alfabetisch Z-A")
                    {
                        goodss = goods.OrderByDescending(o=>o.Naam).ToList();
                        ViewBag.sel2 = sel2;
                    }
                }
                else
                {
                    goodss = goods.ToList();
                }
                var goodsList = goodss.Skip(((P*16)-16)).Take(16).ToList();
                return View(goodsList);
            }
        }
        public ActionResult infoPagina()
        {
            ViewBag.Message = "Information";
            return View();
        }
    }
}