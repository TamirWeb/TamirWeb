using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class İlçe
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int İlid { get; set; }
        public virtual İl İl { get; set; }
    }
}