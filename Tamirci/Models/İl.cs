using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class İl
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection <İlçe> İlçes { get; set; }
    }
}