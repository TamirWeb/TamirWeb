using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamirci.Models;

namespace Tamirci.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        Context c = new Context();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Mesajlar a)
        {
            a.control = false;
            a.Active = false;
            c.Mesajlars.Add(a);
            c.SaveChanges();
            TempData["MesajGonderildi"] = "Başarı!Mesajınız Bize Ulaştı.En Kısa sürede değerlendirilecektir.Vakit ayırdığınız için teşekkürler:)";
            return View();

        }
        public PartialViewResult Section1()
        {
            var dgr = c.Hakkımızdas.Where(x => x.IsDeleted == false).Take(1).ToList();
            return PartialView(dgr);
        }
        [HttpGet]
        public PartialViewResult Puanlama2(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Puanlama2(Puan a)
        {
            a.Tamirciid = a.Tamirciid;
            a.Durum = false;
            c.Puans.Add(a);
            c.SaveChanges();
            return PartialView();
        }
    }
}