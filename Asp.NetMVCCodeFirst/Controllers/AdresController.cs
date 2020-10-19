using Asp.NetMVCCodeFirst.Models;
using Asp.NetMVCCodeFirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Asp.NetMVCCodeFirst.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult YeniAdres() // Sayfa yüklendiğinde çalışır.
        {
            DataBaseContext db = new DataBaseContext();

            //List<Kisiler> kisiler = db.Kisiler.ToList();

            //List<SelectListItem> kisilerList = new List<SelectListItem>();
            //foreach (Kisiler kisi in kisiler)
            //{
            //    SelectListItem item = new SelectListItem();
            //    item.Text = kisi.Ad + " " + kisi.Soyad; // Görünen değer.
            //    item.Value = kisi.Id.ToString(); // Arkada tutulan değer.

            //    kisilerList.Add(item);
            //}

            // Ya da yukarıda olan kodun kısa yazımı ( Linq ile ) ; 

            List<SelectListItem> kisilerList = (from kisi in db.Kisiler.ToList()
                                                select new SelectListItem()
                                                {
                                                    Text = kisi.Ad + " " + kisi.Soyad,
                                                    Value = kisi.Id.ToString()
                                                }).ToList();


            TempData["kisiler"] = kisilerList; // ViewBag ile sayfaya gönderilir.
            ViewBag.kisiler = kisilerList;
            return View();
        }

        [HttpPost]
        public ActionResult YeniAdres(Adresler adres)
        {
            DataBaseContext db = new DataBaseContext();

            /* Kisiler tablosunda ki kisi.Id ile seçilmiş olan kisi.Id ile aynı olanı getir. 
             * FirstOrDefault = silinmiş olmasına rağmen var ise ilk kişiyi getir, yoksa null döndür.
            */
            Kisiler kisi = db.Kisiler.Where(x => x.Id == adres.Kisi.Id).FirstOrDefault();

            if (kisi != null)
            {
                adres.Kisi = kisi; // Yukarı db'den bulduğumuz kişiyi atadık.
                db.Adresler.Add(adres);

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres kaydedilmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Adres kaydedilememiştir.";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }

        public ActionResult Duzenle(int? adresId) // Güncellenicek adresin Id'sini ve o kişinin adını almak için GET işlemi.
        {
            Adresler gelenAdres = null;

            if (adresId != null)
            {
                DataBaseContext db = new DataBaseContext();
                List<SelectListItem> kisilerList = (from kisi in db.Kisiler.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = kisi.Ad + " " + kisi.Soyad,
                                                        Value = kisi.Id.ToString()
                                                    }).ToList();
                TempData["kisiler"] = kisilerList;
                ViewBag.kisiler = kisilerList;

                gelenAdres = db.Adresler.Where(x => x.Id == adresId).FirstOrDefault();
            }

            return View(gelenAdres);
        }

        [HttpPost]
        public ActionResult Duzenle(Adresler guncellenicekAdres, int? adresId)
        {
            DataBaseContext db = new DataBaseContext();

            Kisiler kisi = db.Kisiler.Where(x => x.Id == guncellenicekAdres.Kisi.Id).FirstOrDefault();
            Adresler adres = db.Adresler.Where(y => y.Id == adresId).FirstOrDefault();

            if (kisi != null)
            {
                adres.Kisi = kisi;
                adres.AdresTanim = guncellenicekAdres.AdresTanim;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Adres güncellenememiştir.";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];

            return View();
        }

        [HttpGet]
        public ActionResult Sil(int? AdresId)
        {
            Adresler adres = null;
            if (AdresId != null)
            {
                DataBaseContext db = new DataBaseContext();
                adres = db.Adresler.Where(x => x.Id == AdresId).FirstOrDefault();
            }

            return View(adres);
        }
        [HttpPost,ActionName("Sil")]
        public ActionResult SilOk(int? AdresId)
        {
            if (AdresId != null)
            {
                DataBaseContext db = new DataBaseContext();
                Adresler adres = db.Adresler.Where(x => x.Id == AdresId).FirstOrDefault();

                db.Adresler.Remove(adres);
                db.SaveChanges();
            }

            return RedirectToAction("HomePage","Home");
        }
    }
}