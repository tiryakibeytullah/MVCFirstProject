using Asp.NetMVCCodeFirst.Models;
using Asp.NetMVCCodeFirst.Models.Managers;
using Asp.NetMVCCodeFirst.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp.NetMVCCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult HomePage()
        {
            DataBaseContext db = new DataBaseContext();
            //List<Kisiler> kisiler = db.Kisiler.ToList(); // Select * from Kisiler

            HomePageViewModel model = new HomePageViewModel();
            model.Kisiler = db.Kisiler.ToList();
            model.Adresler = db.Adresler.ToList();

            return View(model);
        }
    }
}