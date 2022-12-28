using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string kategori { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Tamirciler> tamircilers { get; set; }

    }
}