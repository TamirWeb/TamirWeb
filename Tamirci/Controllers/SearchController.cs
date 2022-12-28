using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamirci.Models;

namespace Tamirci.Controllers
{
    public class SearchController : Controller
    {
        Context c = new Context();

            public ActionResult Ara(String k,int sayfa=1)
            {
            var search= k.ToUpper();
                if (!string.IsNullOrEmpty(k))
                {
                  var deger = c.Tamircilers.Where(a => a.TamirciAdı.Contains(search) && a.Tamirci_Aktiflik == true).ToList().ToPagedList(sayfa, 8); ;
                return View(deger);
               }
                return View();

            }
        }
}