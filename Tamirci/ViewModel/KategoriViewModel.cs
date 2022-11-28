using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tamirci.ViewModel
{
    public class KategoriViewModel
    {
        public int Kategoriid { get; set; }
        public List<SelectListItem> Kategorilist { get; set; }
    }

}