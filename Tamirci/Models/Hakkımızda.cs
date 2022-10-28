using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Hakkımızda
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Başlığı boş bırakmayınız!")]
        public string Baslik { get; set; }
        [Required(ErrorMessage = "Hakkımızda yazısını boş bırakmayınız!")]
        public string HakkımızdaYazısı { get; set; }
    }
}