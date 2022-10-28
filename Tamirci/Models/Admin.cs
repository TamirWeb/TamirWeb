using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Admin İsmini boş bırakmayınız!")]
        public string Adminİsim { get; set; }
        [Required(ErrorMessage = "Admin Mailini boş bırakmayınız!")]
        [EmailAddress(ErrorMessage = "Hatalı e-posta adresi")]
        public string AdminMail { get; set; }
        [Required(ErrorMessage = "Admin Telefonunu boş bırakmayınız!")]
       
        public string AdmiTelefon{ get; set; }
        [Required(ErrorMessage = "Admin Kullanıcı Adını boş bırakmayınız!")]
        public string AdminİsimKullanıcıAd { get; set; }
        [Required(ErrorMessage = "Admin Şifreyi boş bırakmayınız!")]
        public string Sifre { get; set; }

    }
}