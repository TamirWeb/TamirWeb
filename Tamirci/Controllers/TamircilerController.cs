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
            return View();
        }
        public ActionResult TamirciDetail()
        {
            return View();
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
            c.Tamircilers.Add(a);
            c.SaveChanges();
            TempData["MesajGonderildi"] = "Başvurunuz bize ulaştı.Uygun görülmesi durumunda Firmanız sayfamızda yer alacaktır.";
            return View();
        }
    }
}