using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tamirci.Models;
using Tamirci.ViewModel;

namespace Tamirci.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Tamirci()
        {
            var dgr = c.Tamircilers.Where(x=>x.IsDeleted==false && x.Tamirci_Aktiflik==true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddTamirci()
        {
            return View();
        }
        public PartialViewResult Kategori()
        {
            KategoriViewModel model = new KategoriViewModel();
            List<Kategori> stkurumlistesi = c.Kategoris.ToList();
            model.Kategorilist = (List<SelectListItem>)(from s in stkurumlistesi
                                                        select new SelectListItem
                                                        {
                                                            Text = s.kategori,
                                                            Value = s.Id.ToString()
                                                        }).ToList();
            model.Kategorilist.Insert(0, new SelectListItem { Text = "Kategori Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }

       [Authorize]
        [HttpPost]
        public ActionResult AddTamirci(Tamirciler a, KategoriViewModel b,İlçeViewModel d)
        {
            a.Kategoriid = b.Kategoriid;
            var dgr1 = c.İls.Find(d.ilid);
            a.Tamirci_İl = dgr1.Name;
            var dgr2 = c.İlçes.Find(d.ilçeid);
            a.Tamirci_İlçe = dgr2.Name;
            a.control = true;
            a.TamirciAdı = a.TamirciAdı.ToUpper();
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength>0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Tamirci_Fotoğraf = "/Content/Style/İmages/" + filename;
            }
            else
            {
                a.Tamirci_Fotoğraf = "/Content/Style/İmages/araba-removebg-preview.png.png";
            }
            a.Tamirci_Puan = 0;
            a.IsDeleted = false;
            c.Tamircilers.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Tamirci", "Admin");
        }
        [Authorize]
        public ActionResult GetTamirci(int id)
        {
           
            var deger = c.Tamircilers.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateTamirci(Tamirciler a,bool check1, bool check2)
        {

            var b = c.Tamircilers.Find(a.ID);
            b.TamirciAdı = a.TamirciAdı;
            b.TamirciMail = a.TamirciMail;
            b.Tamirci_Adres = a.Tamirci_Adres;
            b.Tamirci_İl = a.Tamirci_İl;
            b.Tamirci_İlçe = a.Tamirci_İlçe;
            b.Tamirci_Telefon = a.Tamirci_Telefon;
            b.Tamirci_Puan = a.Tamirci_Puan;
            b.Tamirci_Tanım = a.Tamirci_Tanım;
            if (check1)
            {
                b.Tamirci_Aktiflik = true;
            }
            if (check2)
            {
                b.Tamirci_Aktiflik = false;
            }
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Tamirci_Fotoğraf = "/Content/Style/İmages/" + filename;
                b.Tamirci_Fotoğraf = a.Tamirci_Fotoğraf;
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Tamirci","Admin");
        }
        public ActionResult RemoveTamirci(int id)
        {
            var b = c.Tamircilers.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Tamirci");
        }
        [Authorize]
        public ActionResult Başvuru()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult GetBaşvuru(int id)
        {
            var deger = c.Tamircilers.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateBaşvuru(Tamirciler a, bool check1, bool check2)
        {
            var b = c.Tamircilers.Find(a.ID);
            b.TamirciAdı = a.TamirciAdı;
            b.TamirciMail = a.TamirciMail;
            b.Tamirci_Adres = a.Tamirci_Adres;
            b.Tamirci_İl = a.Tamirci_İl;
            b.Tamirci_İlçe = a.Tamirci_İlçe;
            b.Tamirci_Telefon = a.Tamirci_Telefon;
            b.Tamirci_Puan = a.Tamirci_Puan;
            b.Tamirci_Tanım = a.Tamirci_Tanım;
            if (check1)
            {
                b.Tamirci_Aktiflik = true;
            }
            if (check2)
            {
                b.Tamirci_Aktiflik = false;
            }
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Tamirci_Fotoğraf = "/Content/Style/İmages/" + filename;
                b.Tamirci_Fotoğraf = a.Tamirci_Fotoğraf;
            }
            c.SaveChanges();
            if (b.Tamirci_Aktiflik = true)
            {
                MailMessage mailim = new MailMessage();
                string Mail = b.TamirciMail;
                mailim.To.Add(Mail);
                mailim.From = new MailAddress("gdelice2244@gmail.com");
                mailim.Subject = "Yeni bir Durum Güncellemeniz Var.";
                mailim.IsBodyHtml = true;
                mailim.Body = "Sayın yetkili, " + "<b>" + a.TamirciAdı + " " + "</b>" + "adlı firma başvuru talebiniz onaylanmıştır.Artık değerli firmanız sayfamızda yer alacaktır.";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("gdelice2244@gmail.com", "aqyebzvykbchfffl");
                try
                {
                    smtp.Send(mailim);
                    TempData["MailYollandı"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";
                }
                catch (Exception ex)
                {
                    TempData["MailYollandı"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
                }
            }
            TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Tamirci", "Başvuru");
        }
        public ActionResult RemoveBaşvuru(int id)
        {
            var b = c.Tamircilers.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Başvuru");
        }




        [Authorize]
        public ActionResult Yorumlar()
        {
            var dgr = c.Yorumlars.Where(x => x.Aktiflik == true && x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult GetYorum(int id)
        {
            var deger = c.Yorumlars.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateYorum(Yorumlar a,bool check1,bool check2)
        {
            var b = c.Yorumlars.Find(a.ID);
            b.GonderenAdSoyad = a.GonderenAdSoyad;
            b.GonderenMail = a.GonderenMail;
            b.Yorumİcerik = a.Yorumİcerik;
            b.YorumTarih = a.YorumTarih;
            if (check1)
            {
                b.Aktiflik = true;
            }
            if (check2)
            {
                b.Aktiflik = false;
            }
            if (b.Aktiflik = true)
            {
                MailMessage mailim = new MailMessage();
                string Mail = a.GonderenMail;
                mailim.To.Add(Mail);
                mailim.From = new MailAddress("gdelice2244@gmail.com");
                mailim.Subject = "Yeni bir Durum Güncellemeniz Var.";
                mailim.IsBodyHtml = true;
                mailim.Body = "Sayın yetkili, " + "<b>" + a.GonderenAdSoyad + " " + "</b>" + "ismi ile yaptığınız yorum onaylanmıştır.Yorumunuz sayfamızda yer almaktadır.";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("gdelice2244@gmail.com", "aqyebzvykbchfffl");
                try
                {
                    smtp.Send(mailim);
                    TempData["MailYollandı"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";
                }
                catch (Exception ex)
                {
                    TempData["MailYollandı"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
                }
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Yorumlar", "Admin");
        }
        public ActionResult RemoveYorum(int id)
        {
            var b = c.Yorumlars.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Yorumlar");
        }
        [Authorize]
        public ActionResult Mesajlar()
        {
            var dgr=c.Mesajlars.Where(x =>x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult GetMesaj(int id)
        {
            var deger = c.Mesajlars.Find(id);
            return View(deger);
        }
        //public ActionResult UpdateMesaj()
        //{

        //    return View();
        //}
        public ActionResult RemoveMesaj(int id)
        {
            var b = c.Mesajlars.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Mesaj Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Mesajlar");
        }
        [Authorize]
        public ActionResult Hakkımızda()
        {
            var dgr = c.Hakkımızdas.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddHakkımızda()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddHakkımızda(Hakkımızda a)
        {
            a.IsDeleted = false;
            c.Hakkımızdas.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Hakkımızda", "Admin");
        }
        [Authorize]
        public ActionResult GetHakkımızda(int id)
        {
            var deger = c.Hakkımızdas.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateHakkımızda(Hakkımızda a)
        {
            var b = c.Hakkımızdas.Find(a.ID);
            b.Baslik = a.Baslik;
            b.HakkımızdaYazısı = a.HakkımızdaYazısı;
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Hakkımızda","Admin");
        }
        public ActionResult RemoveHakkımızda(int id)
        {
            var b = c.Hakkımızdas.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Hakkımızda");
        }
        public PartialViewResult Applications_notices()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false  && x.Tamirci_Aktiflik==false && x.control==false).Take(6).ToList();
            return PartialView(dgr);
        }
    }
}