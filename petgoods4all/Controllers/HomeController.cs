using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using petgoods4all.Models;


namespace petgoods4all.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public class AccountReview{
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

            if (! averageRating.Any())
            {
                ViewBag.AverageRating = 0;
            }
            else
            {
                var average = (from r in db.Review where r.ProductId == identication select r.StarRating).Average();
                ViewBag.AverageRating = average;
            }

            return View(product.ToList());
        }

        [HttpPost]
        public ActionResult Productpage()
        {
            int identication= Int32.Parse(HttpContext.Request.Query["identication"].ToString());
            using (var db = new ModelContext())
            {
                int pID = (from m in db.Voorraad where m.Id == identication select m.Id).Single();
                int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
                var UID = HttpContext.Session.GetInt32("UID");
                if (uID != 0)
                {
                    int MaxId;
                    var result = from m in db.Wishlist select m.id;
                    if( !result.Any() )
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
                        productid = pID
                    };
                    db.Wishlist.Add(n);
                    db.SaveChanges();
                }
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
                                index = index + 1;
                            }
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
        public ActionResult Wishpage(string Option, int pIDD)
        {
            //Dit is wel echt slecht gescriptmaarja ik weet ook niet anders
            if(Option == "+ Naar winkelwagen")
            {
                //Nog niet mogelijk
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
        public IActionResult ProductBrowsen(string D, int P = 1)
        {
            ViewBag.Message = "Producten";
            using (var db = new ModelContext())
            {
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
                var goodsList = goods.Skip(((P*16)-16)).Take(16).ToList();
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