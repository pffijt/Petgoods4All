using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using petgoods4all.Models;

namespace petgoods4all.Controllers
{
    public class WishlistController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    
        
    }
}