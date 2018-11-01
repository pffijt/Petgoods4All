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
        public List<int> Related(int indentication)
        {
            List<int> productsID = new List<int>();
            using (var db = new ModelContext())
            {
                string getType = (from m in db.Voorraad where m.Id == indentication select m.Dier).Single();
                string getSub = (from m in db.Voorraad where m.Id == indentication select m.Subklasse).Single();
                var getRelated = (from m in db.Voorraad where m.Dier == getType && m.Subklasse == getSub && m.Id != indentication select m.Id).ToList();
    
                if(getRelated.Count() < 3 || getRelated == null)
                {
                    getRelated = (from m in db.Voorraad where m.Dier == getType  && m.Id != indentication select m.Id).ToList();
                }
                productsID = ((getRelated.OrderBy(x => Guid.NewGuid())).Take(3).ToList());

            }

            return productsID;
        }
        public ActionResult Productpage(int identication = 1)
        {
            ViewBag.Message = "Product page";
            ViewBag.ProductId = identication;
            using (var db = new ModelContext())
            {
                //De hoofdproduct en 3 gerelateerde
                List<int> relatedProducts = Related(identication);
                foreach(var item in relatedProducts)
                {
                    Console.WriteLine(item.ToString());
                }           

                petgoods4all.Models.Voorraad products = (from m in db.Voorraad where m.Id == identication select m).Single();
                List<petgoods4all.Models.Voorraad> otherProducts = (from m in db.Voorraad where m.Id == relatedProducts[0] 
                || m.Id == relatedProducts[1] || m.Id == relatedProducts[2] select m).ToList();
                otherProducts.Add(products);

                return View(otherProducts);
            }
        }

        [HttpPost]
        public ActionResult Productpage()
        {
            int identication= Int32.Parse(HttpContext.Request.Query["identication"].ToString());
            using (var db = new ModelContext())
            {
                int pID = (from m in db.Voorraad where m.Id == identication select m.Id).Single();
                int uID = (HttpContext.Session.GetInt32("UID")).GetValueOrDefault(0);
                if(uID != 0)
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
                return View(product.ToList());
            }
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