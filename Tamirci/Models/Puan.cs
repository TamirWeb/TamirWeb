using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Puan
    {
        [Key]
        public int ID { get; set; }
        public bool Durum { get; set; }
        public int Tamirciid { get; set; }
        public virtual Tamirciler Tamirci { get; set; }
    }
}