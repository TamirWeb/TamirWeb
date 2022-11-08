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
    }
}