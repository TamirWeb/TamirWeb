using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Yorumlar
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Adınız boş bırakmayınız!")]
        public string GonderenAdSoyad { get; set; }
        [Required(ErrorMessage = "Mailinizi boş bırakmayınız!")]
        [EmailAddress(ErrorMessage = "Hatalı e-posta adresi")]
        public string GonderenMail { get; set; }
        public bool Aktiflik { get; set; }
        [Required(ErrorMessage = "Yorum içeriğini boş bırakmayınız!")]
        public string Yorumİcerik{ get; set; }
        public string YorumTarih{ get; set; }
        public virtual Tamirciler Tamirci { get; set; }
    }
}