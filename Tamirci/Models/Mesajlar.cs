using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
	public class Mesajlar
	{
		[Key]
		public int ID { get; set; }
		[Required(ErrorMessage = "Adınızı boş bırakmayınız!")]
		public string MesajGonderenAdSoyad{ get; set; }
		[Required(ErrorMessage = "Mailinizi boş bırakmayınız!")]
		[EmailAddress(ErrorMessage = "Hatalı e-posta adresi")]
		public string MesajGonderenMail { get; set; }
		[Required(ErrorMessage = "Mesaj başlığını boş bırakmayınız!")]
		public string Mesajbaslik { get; set; }
		[Required(ErrorMessage = "Mesajınızı boş bırakmayınız!")]
		public string Mesaj { get; set; }
		public bool IsDeleted { get; set; }
	}
}