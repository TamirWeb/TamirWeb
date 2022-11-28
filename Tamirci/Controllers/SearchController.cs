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

            public ActionResult Ara(String k)
            {
            var search= k.ToUpper();

                var deger = c.Tamircilers.ToList();

                if (!string.IsNullOrEmpty(k))
                {
                    deger = deger.Where(a => a.TamirciAdı.Contains(search) && a.Tamirci_Aktiflik == true).ToList();
                }
                return View(deger);

            }
        }
}