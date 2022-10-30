using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tamirci.Models;

namespace Tamirci.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Tamirci()
        {
            var dgr = c.Tamircilers.Where(x=>x.IsDeleted==false).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddTamirci()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddTamirci(Tamirciler a)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Tamirci_Fotoğraf = "~/Content/Style/İmages/" + filename;
            }
            a.Tamirci_Puan = 0;
            a.IsDeleted = false;
            c.Tamircilers.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Tamirci", "Admin");
        }
        [Authorize]
        public ActionResult GetTamirci(int id)
        {
            var deger = c.Tamircilers.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateTamirci(Tamirciler a,bool check1, bool check2)
        {
            var b = c.Tamircilers.Find(a.ID);
            b.TamirciAdı = a.TamirciAdı;
            b.TamirciMail = a.TamirciMail;
            b.Tamirci_Adres = a.Tamirci_Adres;
            b.Tamirci_İl = a.Tamirci_İl;
            b.Tamirci_Telefon = a.Tamirci_Telefon;
            b.Tamirci_Puan = a.Tamirci_Puan;
            b.Tamirci_Tanım = a.Tamirci_Tanım;
            if (check1)
            {
                b.Tamirci_Aktiflik = true;
            }
            if (check2)
            {
                b.Tamirci_Aktiflik = false;
            }
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Tamirci_Fotoğraf = "/Content/Style/İmages/" + filename + uzanti;
                b.Tamirci_Fotoğraf = a.Tamirci_Fotoğraf;
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Tamirci","Admin");
        }
        public ActionResult RemoveTamirci(int id)
        {
            var b = c.Tamircilers.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Tamirci");
        }
        [Authorize]
        public ActionResult Yorumlar()
        {
            var dgr = c.Yorumlars.Where(x => x.Aktiflik == true && x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult GetYorum(int id)
        {
            var deger = c.Yorumlars.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateYorum(Yorumlar a,bool check1,bool check2)
        {
            var b = c.Yorumlars.Find(a.ID);
            b.GonderenAdSoyad = a.GonderenAdSoyad;
            b.GonderenMail = a.GonderenMail;
            b.Yorumİcerik = a.Yorumİcerik;
            b.YorumTarih = a.YorumTarih;
            if (check1)
            {
                b.Aktiflik = true;
            }
            if (check2)
            {
                b.Aktiflik = false;
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Yorumlar", "Admin");
        }
        public ActionResult RemoveYorum(int id)
        {
            var b = c.Yorumlars.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Yorumlar");
        }
        [Authorize]
        public ActionResult Mesajlar()
        {
            var dgr=c.Mesajlars.Where(x =>x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult GetMesaj(int id)
        {
            var deger = c.Mesajlars.Find(id);
            return View(deger);
        }
        //public ActionResult UpdateMesaj()
        //{

        //    return View();
        //}
        public ActionResult RemoveMesaj(int id)
        {
            var b = c.Mesajlars.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Mesaj Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Mesajlar");
        }
        [Authorize]
        public ActionResult Hakkımızda()
        {
            var dgr = c.Hakkımızdas.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddHakkımızda()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddHakkımızda(Hakkımızda a)
        {
            a.IsDeleted = false;
            c.Hakkımızdas.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Hakkımızda", "Admin");
        }
        [Authorize]
        public ActionResult GetHakkımızda(int id)
        {
            var deger = c.Hakkımızdas.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateHakkımızda(Hakkımızda a)
        {
            var b = c.Hakkımızdas.Find(a.ID);
            b.Baslik = a.Baslik;
            b.HakkımızdaYazısı = a.HakkımızdaYazısı;
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Hakkımızda","Admin");
        }
        public ActionResult RemoveHakkımızda(int id)
        {
            var b = c.Hakkımızdas.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Hakkımızda");
        }
    }
}