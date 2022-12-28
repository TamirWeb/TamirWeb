using NLog;
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
        private Logger _logcu = LogManager.GetCurrentClassLogger();
        // GET: Admin
        Context c = new Context();
        [Authorize]
        public ActionResult Tamirci()
        {
            var dgr = c.Tamircilers.Where(x=>x.IsDeleted==false && x.Tamirci_Aktiflik==true).ToList();
            _logcu.Error("Tamirciler Admin Panelinde Listelendi");
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArşivTamirci()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == true && x.Tamirci_Aktiflik == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddTamirci()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddTamirci(Tamirciler a, KategoriViewModel b, İlçeViewModel d)
        {
            a.Kategoriid = b.Kategoriid;
            var dgr1 = c.İls.Find(d.ilid);
            a.Tamirci_İl = dgr1.Name;
            var dgr2 = c.İlçes.Find(d.ilçeid);
            a.Tamirci_İlçe = dgr2.Name;
            a.control = true;
            a.TamirciAdı = a.TamirciAdı.ToUpper();
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
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
        public ActionResult UpdateTamirci(Tamirciler a, bool check1, bool check2)
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
            b.control = a.control;
           
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
            return RedirectToAction("Tamirci", "Admin");
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
        public ActionResult ReloadTamirci(int id)
        {
            var b = c.Tamircilers.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivTamirci");
        }
        public ActionResult DeleteTamirci(int id)
        {
            var b = c.Tamircilers.Find(id);
            c.Tamircilers.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Tamirci Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArşivTamirci");
        }
        public PartialViewResult Kategori()
        {
            KategoriViewModel model = new KategoriViewModel();
            List<Kategori> stkurumlistesi = c.Kategoris.Where(x=>x.IsDeleted==false).ToList();
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
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


      
        [Authorize]
        public ActionResult Başvuru()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArşivBaşvuru()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == true && x.Tamirci_Aktiflik == false).ToList();
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
            return RedirectToAction("Başvuru", "Admin");
        }
        public ActionResult RemoveBaşvuru(int id)
        {
            var b = c.Tamircilers.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Silindi";
            return RedirectToAction("Başvuru", "Admin");
        }
        public ActionResult ReloadBaşvuru(int id)
        {
            var b = c.Tamircilers.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Tamirci Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivBaşvuru", "Admin");
        }
        public ActionResult DeleteBaşvuru(int id)
        {
            var b = c.Tamircilers.Find(id);
            c.Tamircilers.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Başvuru Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArşivBaşvuru");
        }


        [Authorize]
        public ActionResult Yorumlar()
        {
            var dgr = c.Yorumlars.Where(x=> x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArşivYorumlar()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == true).ToList();
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
            c.SaveChanges();
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
        public ActionResult ReloadYorum(int id)
        {
            var b = c.Yorumlars.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivYorumlar");
        }
        public ActionResult DeleteYorum(int id)
        {
            var b = c.Yorumlars.Find(id);
            c.Yorumlars.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Yorum Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArşivYorumlar");
        }

        [Authorize]
        public ActionResult Mesajlar()
        {
            var dgr=c.Mesajlars.Where(x =>x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArşivMesajlar()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }

        [Authorize]
        public ActionResult GetMesaj(int id)
        {
            var deger = c.Mesajlars.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
         public PartialViewResult Adminler()
        {
            var dgr = c.Admins.Where(x=>x.IsDeleted==false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddToDo()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddToDo(ToDo a)
        {
            a.Status = false;
            c.ToDos.Add(a);
            c.SaveChanges();
            TempData["Alert1Message"] = "Görev Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Index","Admin");
        }
        [Authorize]
        public PartialViewResult ToDoList()
        {
            var dgr = c.ToDos.Where(x=>x.Status==false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult ToDo()
        {
            var dgr = c.ToDos.Where(x => x.Status == true).ToList();
            return PartialView(dgr);
        }
        public ActionResult RemoveToDo(int id)
        {
            var b = c.ToDos.Find(id);
            c.ToDos.Remove(b);
            c.SaveChanges();
                TempData["Alert2Message"] = "Görev Bilgileri Başarı İle Silindi";
            
            return RedirectToAction("");
        }
        public ActionResult UpdateToDo(int id)
        {
            var b = c.ToDos.Find(id);
            b.Status = true;
            c.SaveChanges();
                TempData["Alert1Message"] = "Görev Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("");
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
   
        public ActionResult DeleteAdmin(int id)
        {
            var b = c.Admins.Find(id);
            c.Admins.Remove(b);
            c.SaveChanges();
                TempData["AlertMessage"] = "Admin Bilgileri Kalıcı Bir Şekilde İle Silindi";
            return RedirectToAction("ArşivAdmin", "Admin");
        }
        public ActionResult DeleteMesaj(int id)
        {
            var b = c.Mesajlars.Find(id);
            c.Mesajlars.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Mesaj Bilgileri Kalıcı Bir Şekilde İle Silindi";
            return RedirectToAction("ArşivMesajlar", "Admin");
        }
        public ActionResult ReloadMesaj(int id)
        {
            var b = c.Mesajlars.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Mesaj Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivMesajlar");
        }
        public ActionResult ReloadAdmin(int id)
        {
            var b = c.Admins.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Admin Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivAdmin","Admin");
        }
        public ActionResult UpdateMesaj(Mesajlar a, bool check1, bool check2)
        {
            var b = c.Mesajlars.Find(a.ID);
            b.ID = a.ID;
            b.MesajGonderenAdSoyad = a.MesajGonderenAdSoyad;
            b.MesajGonderenAdSoyad = a.Mesajbaslik;
            b.Mesaj = a.Mesaj;
            if (check1)
            {
                b.Active = true;
            }
            if (check2)
            {
                b.Active = false;
            }
            c.SaveChanges();
            TempData["Alert1Message"] = "Mesaj Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Mesajlar","Admin");
        }
        [Authorize]
        public PartialViewResult SonBasvuru()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.control==false && x.Tamirci_Aktiflik==false).Take(6).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult SonMesaj()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.control == false).Take(6).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult SonYorum()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == false && x.control == false).Take(6).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalYorum()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == false && x.Aktiflik==true && x.control==true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalMesaj()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalBaşvuru()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.control == false && x.Tamirci_Aktiflik==false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalTamirci()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalTıklanma()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.control == true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalAdmin()
        {
            var dgr = c.Admins.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public ActionResult Hakkımızda()
        {
            var dgr = c.Hakkımızdas.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }

        [Authorize]
        public ActionResult ArşivHakkımızda()
        {
            var dgr = c.Hakkımızdas.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArşivAdmin()
        {
            var dgr = c.Admins.Where(x => x.IsDeleted == true).ToList();
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
        public ActionResult DeleteHakkımızda(int id)
        {
            var b = c.Hakkımızdas.Find(id);
            c.Hakkımızdas.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Yazısı Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArşivHakkımızda");
        }

        public ActionResult ReloadHakkımızda(int id)
        {
            var b = c.Hakkımızdas.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivHakkımızda");
        }
        public PartialViewResult Applications_notices()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false  && x.Tamirci_Aktiflik==false && x.control==false).Take(6).ToList();
            return PartialView(dgr);
        }


        [Authorize]
        public ActionResult Kategoriler()
        {
            var dgr = c.Kategoris.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArşivKategoriler()
        {
            var dgr = c.Kategoris.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddKategori()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddKategori(Kategori a)
        {
            a.IsDeleted = false;
            c.Kategoris.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Kategori Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Kategoriler", "Admin");
        }
        [Authorize]
        public ActionResult GetKategori(int id)
        {
            var deger = c.Kategoris.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateKategori(Kategori a)
        {
            var b = c.Kategoris.Find(a.Id);
            b.kategori = a.kategori;
            c.SaveChanges();
            TempData["AlertMessage"] = "Kategori Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Kategoriler", "Admin");
        }
        public ActionResult RemoveKategori(int id)
        {
            var b = c.Kategoris.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Kategori Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Kategoriler");
        }
        public ActionResult DeleteKategori(int id)
        {
            var b = c.Kategoris.Find(id);
            c.Kategoris.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Kategori Bilgileri Kalıcı bir şekilde İle Silindi";
            return RedirectToAction("ArşivKategoriler");
        }
            public ActionResult ReloadKategori(int id)
        {
            var b = c.Kategoris.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Kategori Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArşivKategoriler");
        }
        [Authorize]
        public ActionResult İl()
        {
            var dgr = c.İls.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult Arşivİl()
        {
            var dgr = c.İls.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Addİl()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Addİl(İl a)
        {
            a.IsDeleted = false;
            a.Name=a.Name.ToUpper();
            c.İls.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "İl Bilgileri Başarı İle Eklendi";
            return RedirectToAction("İl", "Admin");
        }
        [Authorize]
        public ActionResult Getİl(int id)
        {
            var deger = c.İls.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult Updateİl(İl a)
        {
            var b = c.İls.Find(a.ID);
            b.Name = a.Name.ToUpper();
            c.SaveChanges();
            TempData["AlertMessage"] = "İl Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("İl", "Admin");
        }
        public ActionResult Removeİl(int id)
        {
            var b = c.İls.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "İl Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("İl");
        }
        public ActionResult Deleteİl(int id)
        {
            var b = c.İls.Find(id);
            c.İls.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "İl Bilgileri Kalıcı bir şekilde İle Silindi";
            return RedirectToAction("Arşivİl");
        }
        public ActionResult Reloadİl(int id)
        {
            var b = c.İls.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "İl Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("Arşivİl");
        }
        [Authorize]
        public ActionResult İlçe()
        {
            var dgr = c.İlçes.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        public ActionResult Arşivİlçe()
        {
            var dgr = c.İlçes.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        public PartialViewResult İlİlçe()
        {
            İlçeViewModel model = new İlçeViewModel();
            List<İl> illiste = c.İls.Where(x=>x.IsDeleted==false).ToList();
            model.illist = (List<SelectListItem>)(from s in illiste
                                                  select new SelectListItem
                                                  {
                                                      Text = s.Name,
                                                      Value = s.ID.ToString()
                                                  }).ToList();

            model.illist.Insert(0, new SelectListItem { Text = "Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Addİlçe()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Addİlçe(İlçe a,İlçeViewModel d)
        {
            a.İlid = d.ilid;
            a.Name=a.Name.ToUpper();
            a.IsDeleted = false;
            c.İlçes.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Eklendi";
            return RedirectToAction("İlçe", "Admin");
        }
        [Authorize]
        public ActionResult Getİlçe(int id)
        {
            var deger = c.İlçes.Find(id);
            return View(deger);
        }
        [Authorize]
        public ActionResult Updateİlçe(İlçe a,İlçeViewModel k)
        {
            var b = c.İlçes.Find(a.ID);
            b.Name = a.Name.ToUpper();
            if (k.ilid != 0)
            {
                b.İlid = k.ilid;
               
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("İlçe", "Admin");
        }
        public ActionResult Removeİlçe(int id)
        {
            var b = c.İlçes.Find(id);
            b.IsDeleted = true;
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("İlçe");
        }
        public ActionResult Deleteİlçe(int id)
        {
            var b = c.İlçes.Find(id);
            c.İlçes.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "İlçe Bilgileri Kalıcı bir şekilde İle Silindi";
            return RedirectToAction("Arşivİlçe");
        }
        public ActionResult Reloadİlçe(int id)
        {
            var b = c.İlçes.Find(id);
            b.IsDeleted = false;
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("Arşivİlçe");
        }

    }
}