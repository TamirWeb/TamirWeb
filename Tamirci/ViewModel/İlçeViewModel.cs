using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tamirci.ViewModel
{
    public class İlçeViewModel
    {
        public İlçeViewModel()
        {
            this.ilçelist = new List<SelectListItem>();
            ilçelist.Add(new SelectListItem { Text = "Seçiniz ", Value = " " });
           
        }
        public int ilid { get; set; }
        public int ilçeid { get; set; }
        public List<SelectListItem> illist { get; set; }
        public List<SelectListItem> ilçelist { get; set; }
    }
}
