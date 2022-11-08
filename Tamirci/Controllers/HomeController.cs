﻿using System;
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
            var dgr=c.Tamircilers.OrderByDescending(x=>x.ID).Take(6).Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }
   
        public PartialViewResult Section2()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false).ToList();
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
            var dgr = c.Tamircilers.OrderByDescending(x=>x.Click).Where(x => x.IsDeleted == false).Take(6).ToList();
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
    }
}