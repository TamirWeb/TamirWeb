using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamirci.Models;

namespace Tamirci.Controllers
{
    public class TamircilerController : Controller
    {
        // GET: Tamirciler
        Context c = new Context();
        public ActionResult Index()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==true).ToList();
            return View(dgr);
        }
        public ActionResult TamirciDetail(int id)
        {
            var dgr = c.Tamircilers.Where(x => x.ID == id).ToList();
            return View(dgr);
        }
        [HttpGet]
        public ActionResult TamirciBaşvuru()
        {
            return View();

        }
        [HttpPost]
        public ActionResult TamirciBaşvuru(Tamirciler a)
        {
            a.Tamirci_Aktiflik = false;
            a.Tamirci_Puan = 0;
            a.Click = 0;
            c.Tamircilers.Add(a);
            c.SaveChanges();
            TempData["MesajGonderildi"] = "Başvurunuz bize ulaştı.Uygun görülmesi durumunda Firmanız sayfamızda yer alacaktır.";
            return View();
        }
        public PartialViewResult Section1(int id)
        {
          var deger = c.Yorumlars.Where(x => x.Tamirci.ID == id).OrderByDescending(x => x.ID).ToList();
            return PartialView(deger);
        }
        [HttpGet]
        public PartialViewResult Yorumekle(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }

        [HttpPost]

        public PartialViewResult Yorumekle(Yorumlar a)
        {
            c.Yorumlars.Add(a);
            c.SaveChanges();
            return PartialView();
        }
    }
}