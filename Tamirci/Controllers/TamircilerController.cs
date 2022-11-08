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
    }
}