using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class ToDo
    {
        [Key]
        public int ID { get; set; }
        public string Explonation { get; set; }
        public bool Status { get; set; }
    }
}