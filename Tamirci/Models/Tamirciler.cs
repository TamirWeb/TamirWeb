using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Tamirciler
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Tamirci Adını boş bırakmayınız!")]
        public string TamirciAdı { get; set; }
        [Required(ErrorMessage = "Bulunduğu ili boş bırakmayınız boş bırakmayınız!")]
        public string Tamirci_İl{ get; set;}
        [Required(ErrorMessage = "Tamirci Adresini boş bırakmayınız!")]
        public string Tamirci_Adres { get; set; }
        [Required(ErrorMessage = "Tamirci Telefonunu boş bırakmayınız!")]
        public string Tamirci_Telefon { get; set; }
        [Required(ErrorMessage = "Tamirci Mailini boş bırakmayınız!")]
        [EmailAddress(ErrorMessage = "Hatalı e-posta adresi")]
        public string TamirciMail { get; set; }
        public string Tamirci_Fotoğraf{ get; set; }
        public float Tamirci_Puan{ get; set; }
        public bool Tamirci_Aktiflik { get; set; }
        [Required(ErrorMessage = "Tamirci tanımını boş bırakmayınız!")]
        public string Tamirci_Tanım { get; set; }
        public ICollection<Yorumlar> Yorumlars { get; set; }
    }
}