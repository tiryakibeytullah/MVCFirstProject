using Asp.NetMVCCodeFirst.Models;
using Asp.NetMVCCodeFirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp.NetMVCCodeFirst.Controllers
{
    public class KisiController : Controller
    {
        // GET: Kisi
        public ActionResult YeniKisi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKisi(Kisiler kisi)
        {
            DataBaseContext db = new DataBaseContext();
            db.Kisiler.Add(kisi);
            int etkilenenKayitSayisi = db.SaveChanges();

            if (etkilenenKayitSayisi > 0)
            {
                ViewBag.Result = "Kişi kaydedilmiştir.";
                ViewBag.Status = "success";
            }
            else
            { 
                ViewBag.Result = "Kişi kaydedilememiştir.";
                ViewBag.Status = "danger";
            }

            return View();
        }

        public ActionResult Duzenle(int? kisiId) // Düzenlenicek kişinin sayfaya yüklenmesi işlemi. Duzenle GET Metodu
        {
            Kisiler gelenKisi = null;
            if (kisiId != null)
            {
                DataBaseContext db = new DataBaseContext();
                gelenKisi = db.Kisiler.Where(x => x.Id == kisiId).FirstOrDefault();
            }
            return View(gelenKisi);
        }

        [HttpPost]
        public ActionResult Duzenle(Kisiler kisi, int? kisiId) // Düzenlenicek kişinin sayfaya yüklenmesi işlemi. Duzenle GET Metodu
        {
            DataBaseContext db = new DataBaseContext();
            Kisiler duzenlenicekKisi = db.Kisiler.Where(x => x.Id == kisiId).FirstOrDefault();

            if (duzenlenicekKisi != null)
            {
                duzenlenicekKisi.Ad = kisi.Ad;
                duzenlenicekKisi.Soyad = kisi.Soyad;
                duzenlenicekKisi.Yas = kisi.Yas;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Kişi güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Kişi güncellenememiştir.";
                    ViewBag.Status = "danger";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Sil(int? kisiId)
        {
            Kisiler kisi = null;
            if (kisiId != null)
            {
                DataBaseContext db = new DataBaseContext();
                kisi = db.Kisiler.Where(x => x.Id == kisiId).FirstOrDefault();
            }
            return View(kisi);
        }

        [HttpPost,ActionName("Sil")]
        public ActionResult SilOk(int? kisiId)
        {
            if (kisiId != null)
            {
                DataBaseContext db = new DataBaseContext();
                Kisiler kisi = db.Kisiler.Where(x => x.Id == kisiId).FirstOrDefault();

                db.Kisiler.Remove(kisi);
                db.SaveChanges();
            }
            return RedirectToAction("HomePage","Home");
        }

    }
}