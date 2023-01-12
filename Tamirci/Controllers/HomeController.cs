using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamirci.Models;

namespace Tamirci.Controllers
{
    public class HomeController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            var dgr = c.Tamircilers.OrderByDescending(x => x.Tamirci_Puan).Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        public PartialViewResult Section1()
        {
            var dgr=c.Tamircilers.OrderByDescending(x=>x.ID).Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==true).Take(6).ToList();
            return PartialView(dgr);
        }
   
        public PartialViewResult Section2()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult Section2_1()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult Section3()
        {
            return PartialView();
        }
        public PartialViewResult Section4()
        {
            var dgr = c.Tamircilers.OrderByDescending(x=>x.Click).Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==true).Take(6).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult Section5()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Yorumlars.Count).Take(6).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult Section6()
        {
            return PartialView();
        }
        public PartialViewResult Section7()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false).Take(6).ToList();
            return PartialView(dgr);
        }

        [HttpGet]
        public PartialViewResult Puanlama1(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Puanlama1(Puan a)
        {
            a.Tamirciid = a.Tamirciid;
            a.Durum = true;
            c.Puans.Add(a);
            c.SaveChanges();
            return PartialView();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Mesajlar()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.Active==true).ToList();
            return View(dgr);
        }
        public ActionResult Site_Usage_Rules()
        {
            return View();
        }
        public ActionResult Copyright()
        {
            return View();
        }
        public PartialViewResult TotalYorum()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == false && x.Aktiflik == true && x.control == true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalBaşvuru()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.control == false && x.Tamirci_Aktiflik == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalTamirci()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalTıklanma()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.control == true).ToList();
            return PartialView(dgr);
        }

    }
}