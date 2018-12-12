using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using petgoods4all.Models;


namespace petgoods4all.Controllers
{
    public class AdminController : Controller
    {
        ModelContext db = new ModelContext();
        // GET: Admin
        [HttpGet]
        public ActionResult ToegangGeweigerd()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);

                if (Admin != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        public class OmzetOrder
        {
            public int Id { get; set; }
            public int AccountId { get; set; }
            public DateTime Datum { get; set; }
            public string Prijs { get; set; }
            public string OrderStatus { get; set; }
            public string DatumString { get; set; }
        }

        public ActionResult Omzet()
        {
            var Orders = (from a in db.Order orderby a.Datum, a.Prijs select a).Distinct();
            var jaren = (from j in db.Order orderby j.Datum.Year select j.Datum.Year).Distinct();
            //foreach (var item in Orders)
            //{
            //    var stringDatum = item.Datum.ToString();
            //    string a = stringDatum.Split('-')[1].ToString();

            //    var Orderss = from b in db.Order
            //        select new OmzetOrder
            //        {
            //            Id = b.Id,
            //            AccountId = b.AccountId,
            //            Datum = b.Datum,
            //            Prijs = b.Prijs,
            //            OrderStatus = b.OrderStatus,
            //            DatumString = a
            //        };
            //}

            //double c = 0;
            double Januari = 0;
            double Februari = 0;
            double Maart = 0;
            double April = 0;
            double Mei = 0;
            double Juni = 0;
            double Juli = 0;
            double Augustus = 0;
            double September = 0;
            double Oktober = 0;
            double November = 0;
            double December = 0;
            decimal JanuariPrijs = 0;
            decimal FebruariPrijs = 0;
            decimal MaartPrijs = 0;
            decimal AprilPrijs = 0;
            decimal MeiPrijs = 0;
            decimal JuniPrijs = 0;
            decimal JuliPrijs = 0;
            decimal AugustusPrijs = 0;
            decimal SeptemberPrijs = 0;
            decimal OktoberPrijs = 0;
            decimal NovemberPrijs = 0;
            decimal DecemberPrijs = 0;
            decimal JanuariInkoopPrijs = 0;
            decimal FebruariInkoopPrijs = 0;
            decimal MaartInkoopPrijs = 0;
            decimal AprilInkoopPrijs = 0;
            decimal MeiInkoopPrijs = 0;
            decimal JuniInkoopPrijs = 0;
            decimal JuliInkoopPrijs = 0;
            decimal AugustusInkoopPrijs = 0;
            decimal SeptemberInkoopPrijs = 0;
            decimal OktoberInkoopPrijs = 0;
            decimal NovemberInkoopPrijs = 0;
            decimal DecemberInkoopPrijs = 0;
            decimal JanuariOmzet = 0;
            decimal FebruariOmzet = 0;
            decimal MaartOmzet = 0;
            decimal AprilOmzet = 0;
            decimal MeiOmzet = 0;
            decimal JuniOmzet = 0;
            decimal JuliOmzet = 0;
            decimal AugustusOmzet = 0;
            decimal SeptemberOmzet = 0;
            decimal OktoberOmzet = 0;
            decimal NovemberOmzet = 0;
            decimal DecemberOmzet = 0;

            foreach (var item in Orders)
            {
                //string datumString = item.Datum.ToString(new System.Globalization.CultureInfo("en-US"));
                //string splittedDatumString = datumString.Split('/')[0].ToString();
                string splittedDatumString = item.Datum.Month.ToString();
                if (splittedDatumString == "1") {
                    double a = Convert.ToDouble(item.Prijs);
                    Januari = Januari + a;
                    double b = (Januari / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Januari);

                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    JanuariInkoopPrijs = e;
                    JanuariPrijs = d;
                    JanuariOmzet = JanuariPrijs - JanuariInkoopPrijs;
                }

                if (splittedDatumString == "2")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Februari = Februari + a;
                    double b = (Februari / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Februari);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    FebruariInkoopPrijs = e;
                    FebruariPrijs = d;
                    FebruariOmzet = FebruariPrijs - FebruariInkoopPrijs;
                }

                if (splittedDatumString == "3")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Maart = Maart + a;
                    double b = (Maart / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Maart);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    MaartInkoopPrijs = e;
                    MaartPrijs = d;
                    MaartOmzet = MaartPrijs - MaartInkoopPrijs;
                }

                if (splittedDatumString == "4")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    April = April + a;
                    double b = (April / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(April);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    AprilInkoopPrijs = e;
                    AprilPrijs = d;
                    AprilOmzet = AprilPrijs - AprilInkoopPrijs;
                }

                if (splittedDatumString == "5")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Mei = Mei + a;
                    double b = (Mei / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Mei);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    MeiInkoopPrijs = e;
                    MeiPrijs = d;
                    MeiOmzet = MeiPrijs - MeiInkoopPrijs;
                }

                if (splittedDatumString == "6")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Juni = Juni + a;
                    double b = (Juni / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Juni);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    JuniInkoopPrijs = e;
                    JuniPrijs = d;
                    JuniOmzet = JuniPrijs - JuniInkoopPrijs;
                }

                if (splittedDatumString == "7")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Juli = Juli + a;
                    double b = (Juli / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Juli);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    JuliInkoopPrijs = e;
                    JuliPrijs = d;
                    JuliOmzet = JuliPrijs - JuliInkoopPrijs;
                }

                if (splittedDatumString == "8")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Augustus = Augustus + a;
                    double b = (Augustus / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Augustus);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    AugustusInkoopPrijs = e;
                    AugustusPrijs = d;
                    AugustusOmzet = AugustusPrijs - AugustusInkoopPrijs;
                }

                if (splittedDatumString == "9")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    September = September + a;
                    double b = (September / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(September);
                    d = Math.Round(d, 2);
                    e = Math.Round(e, 2);

                    SeptemberInkoopPrijs = e;
                    SeptemberPrijs = d;
                    SeptemberOmzet = SeptemberPrijs - SeptemberInkoopPrijs;
                }

                if (splittedDatumString == "10")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    Oktober = Oktober + a;
                    double b = (Oktober / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(Oktober);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    OktoberInkoopPrijs = e;
                    OktoberPrijs = d;
                    OktoberOmzet = OktoberPrijs - OktoberInkoopPrijs;
                }

                if (splittedDatumString == "11")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    November = November + a;
                    double b = (November / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(November);
                    Math.Round(d, 2);
                    Math.Round(e, 2);

                    NovemberInkoopPrijs = e;
                    NovemberPrijs = d;
                    NovemberOmzet = NovemberPrijs - NovemberInkoopPrijs;
                }

                if (splittedDatumString == "12")
                {
                    double a = Convert.ToDouble(item.Prijs);
                    December = December + a;
                    double b = (December / 100) * 80;
                    decimal e = Convert.ToDecimal(b);
                    decimal d = Convert.ToDecimal(December);
                    d = Math.Round(d, 2);
                    e = Math.Round(e, 2);


                    DecemberInkoopPrijs = e;
                    DecemberPrijs = d;
                    DecemberOmzet = DecemberPrijs - DecemberInkoopPrijs;
                }
            }

            ViewBag.Januari = JanuariPrijs;
            ViewBag.Februari = FebruariPrijs;
            ViewBag.Maart = MaartPrijs;
            ViewBag.April = AprilPrijs;
            ViewBag.Mei = MeiPrijs;
            ViewBag.Juni = JuniPrijs;
            ViewBag.Juli = JuliPrijs;
            ViewBag.Augustus = AugustusPrijs;
            ViewBag.September = SeptemberPrijs;
            ViewBag.Oktober = OktoberPrijs;
            ViewBag.November = NovemberPrijs;
            ViewBag.December = DecemberPrijs;
            ViewBag.Orders = Orders;
            ViewBag.JanuariInkoop = JanuariInkoopPrijs;
            ViewBag.FebruariInkoop = FebruariInkoopPrijs;
            ViewBag.MaartInkoop = MaartInkoopPrijs;
            ViewBag.AprilInkoop = AprilInkoopPrijs;
            ViewBag.MeiInkoop = MeiInkoopPrijs;
            ViewBag.JuniInkoop = JuniInkoopPrijs;
            ViewBag.JuliInkoop = JuliInkoopPrijs;
            ViewBag.AugustusInkoop = AugustusInkoopPrijs;
            ViewBag.SeptemberInkoop = SeptemberInkoopPrijs;
            ViewBag.OktoberInkoop = OktoberInkoopPrijs;
            ViewBag.NovemberInkoop = NovemberInkoopPrijs;
            ViewBag.DecemberInkoop = DecemberInkoopPrijs;
            ViewBag.JanuariOmzet = JanuariOmzet;
            ViewBag.FebruariOmzet = FebruariOmzet;
            ViewBag.MaartOmzet = MaartOmzet;
            ViewBag.AprilOmzet = AprilOmzet;
            ViewBag.MeiOmzet = MeiOmzet;
            ViewBag.JuniOmzet = JuniOmzet;
            ViewBag.JuliOmzet = JuliOmzet;
            ViewBag.AugustusOmzet = AugustusOmzet;
            ViewBag.SeptemberOmzet = SeptemberOmzet;
            ViewBag.OktoberOmzet = OktoberOmzet;
            ViewBag.NovemberOmzet = NovemberOmzet;
            ViewBag.DecemberOmzet = DecemberOmzet;
            ViewBag.Orders = Orders;
            ViewBag.jaren = jaren;

            return View();
        }

        public ActionResult AdminInstellingen()
        {
            return View();
        }

        public List<Account> AccountSearch(string accountSearch)
        {
            using (db)
            {
                var allaccounts = db.Account;
                var foundaccounts = allaccounts.Where(x => x.email.Contains(accountSearch)).ToList();
                return foundaccounts;
            }
        }
        public List<Voorraad> ProductSearch(string productSearch)
        {
            var allproducts = db.Voorraad.OrderBy(x => x.Dier);
            var foundproducts = allproducts.Where(x => x.Naam.Contains(productSearch)).ToList();
            return foundproducts;
        }

        //Roept pagina op met de lijsten van Account of Product
        public ActionResult AdminKlantIndex(string accountSearch, int P = 1)
        {
            using (db)
            {
                ViewBag.firstnum = (P * 16) - 15;
                ViewBag.secondnum = 16 * P;
                ViewBag.paginationindex = P;

                var accounts = db.Account.ToList();

                ViewBag.count = accounts.Count();
                if (ViewBag.secondnum > ViewBag.count)
                {
                    ViewBag.secondnum = ViewBag.count;
                }

                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);
                if (Admin != null)
                {
                    var foundaccountlist = AccountSearch(accountSearch);
                    if (foundaccountlist.Count() == 0)
                    {
                        var paccounts = accounts.Skip(((P * 16) - 16)).Take(16);
                        var accountlist = paccounts.ToList();
                        var oaccountlist = accountlist.OrderBy(x => x.email);
                        return View(oaccountlist);
                    }
                    else
                    {
                        var oaccountlist = foundaccountlist.OrderBy(x => x.email);
                        var accountlist = oaccountlist.Skip(((P * 16) - 16)).Take(16);
                        return View(accountlist);
                    }
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }
    
        public ActionResult AdminVoorraadIndex(string productSearch, int P=1)
        {
            using (db)
            {
                ViewBag.firstnum = (P * 16) - 15;
                ViewBag.secondnum = 16 * P;
                ViewBag.paginationindex = P;

                var products =  db.Voorraad.ToList();

                ViewBag.count = products.Count();
                if (ViewBag.secondnum > ViewBag.count)
                {
                    ViewBag.secondnum = ViewBag.count;
                }

                var productList = products.OrderBy(x => x.Naam);
                

                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);
                if (Admin != null)
                {
                    var foundproductlist = ProductSearch(productSearch);
                    if (foundproductlist.Count() == 0)
                    {
                        var pproducts = products.Skip(((P * 16) - 16)).Take(16);
                        var productlist = pproducts.OrderBy(x => x.Naam);
                        return View(productlist);
                    }
                    else
                    {
                        var oproductlist = foundproductlist.OrderBy(x => x.Naam);
                        var productlist = oproductlist.Skip(((P * 16) - 16)).Take(16);
                        return View(productlist);
                    }
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }
        //Roept pagina op met de Details van Account of Product
        public ActionResult AdminKlantDetails(int? id)
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);
                Account account = db.Account.FirstOrDefault(x => x.id == id);

                if (Admin != null)
                {
                    return View(account);
                }
                else
                {

                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        public ActionResult AdminVoorraadDetails(int? id)
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);
                Voorraad voorraad = db.Voorraad.FirstOrDefault(x => x.Id == id);

                if (Admin != null)
                {
                    return View(voorraad);
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        //Roept pagina op voor het aanmaken van een Account of Product
        public ActionResult AdminCreateAccount()
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);

                if (Admin != null)
                {

                    return View();
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        public ActionResult AdminCreateVoorraad()
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);

                if (Admin != null)
                {

                    return View();
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        //Roept pagina op voor de Details van een Account of Product
        public ActionResult AdminKlantEdit(int? id)
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);
                Account account = db.Account.FirstOrDefault(x => x.id == id);

                if (Admin != null)
                {
                    return View(account);
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        public ActionResult AdminVoorraadEdit(int? id)
        {
            using (db)
            {
                var UserID = HttpContext.Session.GetInt32("UID");
                var Admin = db.Account.FirstOrDefault(x => x.id == UserID && x.Admin == true);
                Voorraad voorraad = db.Voorraad.FirstOrDefault(x => x.Id == id);

                if (Admin != null)
                {
                    return View(voorraad);
                }
                else
                {
                    return RedirectToAction("ToegangGeweigerd");
                }
            }
        }

        //Creeren nieuw Account/Product als je op de Opslaan knop klikt
        [HttpPost]
        public ActionResult AdminCreateAccountSave(string email, string postcode, string provincie, string huisnummer, string achternaam, string voornaam, string telefoonnummer, string straatnaam, bool Admin, string password, bool IsEmailVerified)
        {
            using (db)
            {
                var result = from acc in db.Account select acc.id;

                var MaxId = result.Max();


                Account a = new Account
                {
                    id = MaxId + 1,
                    email = email,
                    Admin = Admin,
                    password = password,
                    voornaam = voornaam,
                    achternaam = achternaam,
                    telefoonnummer = telefoonnummer,
                    straatnaam = straatnaam,
                    postcode = postcode,
                    provincie = provincie,
                    IsEmailVerified = IsEmailVerified,
                    huisnummer = huisnummer
                };
                db.Account.Add(a);
                db.SaveChanges();
            } 
            return RedirectToAction("AdminKlantIndex");
        }

        public ActionResult AdminCreateVoorraadSave(string Naam, string Dier, string Subklasse, string Prijs, int Kwantiteit)
        {
            using (db)
            {
                var result = from v in db.Voorraad select v.Id;

                var MaxId = result.Max();

                Voorraad a = new Voorraad
                {
                    Id = MaxId + 1,
                    Naam = Naam,
                    Dier = Dier,
                    Subklasse = Subklasse,
                    Prijs = Prijs,
                    Kwantiteit = Kwantiteit
                };
                db.Voorraad.Add(a);
                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex");
        }

        //Updaten Account/Product als je op de opslaan knop klikt in de Edit pagina
        public ActionResult AdminKlantEditSave(int? id, string voornaam,bool IsEmailVerified, bool Admin, string huisnummer, string provincie, string postcode,  string achternaam, string email, string straatnaam, string telefoonnummer)
        {
            using (db)
            {
                var accountToUpdate = db.Account.FirstOrDefault(x => x.id == id);

                accountToUpdate.achternaam = achternaam;
                accountToUpdate.voornaam = voornaam;
                accountToUpdate.telefoonnummer = telefoonnummer;
                accountToUpdate.straatnaam = straatnaam;
                accountToUpdate.email = email;
                accountToUpdate.huisnummer = huisnummer;
                accountToUpdate.provincie = provincie;
                accountToUpdate.postcode = postcode;
                accountToUpdate.Admin = Admin;
                accountToUpdate.IsEmailVerified = IsEmailVerified;

                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }


        public ActionResult AdminVoorraadEditSave(int? id, string Naam, string Dier, string Subklasse, int Kwantiteit, string Prijs, string image)
        {
            using (db)
            {
                var voorraadToUpdate = db.Voorraad.Find(id);

                voorraadToUpdate.Naam = Naam;
                voorraadToUpdate.Dier = Dier;
                voorraadToUpdate.Subklasse = Subklasse;
                voorraadToUpdate.Kwantiteit = Kwantiteit;
                voorraadToUpdate.Prijs = Prijs;
                voorraadToUpdate.image = image;

                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex");
        }


        //Deleten Account/Product waarbij je op de knop klikt
        public ActionResult AdminKlantDelete(int? id)
        {
            using (db)
            {
                var accountToDelete = db.Account.FirstOrDefault(x => x.id == id); 
                db.Account.Remove(accountToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AdminKlantIndex");
        }

        public ActionResult AdminVoorraadDelete(int? id)
        {
            using (db)
            {
                var voorraadToDelete = db.Voorraad.FirstOrDefault(x => x.Id == id);
                db.Voorraad.Remove(voorraadToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AdminVoorraadIndex");
        }

        public ActionResult OmzetProducten(int Datum, DateTime DatumYear)
        { 

            var db = new ModelContext();

            //per maand de verkochte producten laten zien skrr
            //var Orders = from m in db.Order where m.Datum == Datum select m;
            //var result = from s in db.OrderedProducts select s;

            var OrdersJoin = from op in db.OrderedProducts
                             join o in db.Order on op.OrderId equals o.Id
                             where o.Datum.Month == Datum && o.Datum.Year == DatumYear.Year
                             select new OrderedProducts
                             {
                                 Id = op.Id,
                                 OrderId = op.OrderId,
                                 ProductId = op.ProductId,
                                 Quantity = op.Quantity
                             };

            List < Voorraad > voorraadList = new List<Voorraad>();

            foreach (var item in OrdersJoin)
            {
                var orderedProductsResult = (from s in db.Voorraad where s.Id == item.ProductId select s).Single();

                voorraadList.Add(new Voorraad
                {
                    Id = orderedProductsResult.Id,
                    Naam = orderedProductsResult.Naam,
                    Dier = orderedProductsResult.Dier,
                    Subklasse = orderedProductsResult.Subklasse,
                    Kwantiteit = item.Quantity,
                    Prijs = orderedProductsResult.Prijs,
                    image = orderedProductsResult.image,
                });

            }

            var totaalPrijsResult = from order in db.Order where order.Datum.Month == Datum && order.Datum.Year == DatumYear.Year select order.Prijs;
            double totaalPrijs = 0;
            foreach(var item in totaalPrijsResult)
            {
                double a = Convert.ToDouble(item);
                totaalPrijs = totaalPrijs + a;
                
            }
            decimal e = Convert.ToDecimal(totaalPrijs);
            
            e= Math.Round(e, 2); 
            e.ToString(new System.Globalization.CultureInfo("en-US"));
            ViewBag.Prijs = e;
            ViewBag.Producten = voorraadList;

            return View();
        }
    }
}


        