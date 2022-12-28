using Tamirci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace Tamirci.Controllers
{
     
    public class LoginController : Controller
    {
        string Username;
        Context c = new Context();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Login(Admin a)
        {
           
            var cyrpto = new SimpleCrypto.PBKDF2();
            var user = c.Admins.Where(x => x.AdminİsimKullanıcıAd == a.AdminİsimKullanıcıAd && x.IsDeleted==false).FirstOrDefault();
            
            //var dgr = c.Admins.FirstOrDefault(x => x.UserName == a.UserName && x.Password == a.Password);
            if (user != null)
            {
                Username = user.AdminMail;
                if (user.Sifre == cyrpto.Compute(a.Sifre, user.Salt))
                {
                    FormsAuthentication.SetAuthCookie(user.AdminİsimKullanıcıAd, false);
                    Session["AdminİsimKullanıcıAd"] = user.AdminİsimKullanıcıAd.ToString();
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    TempData["WrongLogin"] = "Kullanıcı Adı Veya Şifreniz Hatalı!!";
                    return RedirectToAction("Login", "Login");
                }

            }
            else
            {
                TempData["WrongLogin"] = "Bu kullanıcı adı ile bir kullanıcı bulunamadı!!";
                return RedirectToAction("Login", "Login");
            }

        }
 
        public ActionResult MyProfile()
        {
            string session = Session["AdminİsimKullanıcıAd"].ToString();
            var dgr = c.Admins.Where(x => x.AdminİsimKullanıcıAd == session).Take(1).ToList();
            return View(dgr);
            Console.WriteLine(Username);
        }
    
        public ActionResult GetMyProfile(int id)
        {
            var deger = c.Admins.Find(id);
            return View(deger);
        }

        public ActionResult Password()
        {
            return View();
        }
        public static string _OnayKodu = "";
        public ActionResult ForgotPassword(string Mail,string Name)
        {
            var dgr2 = c.Admins.Where(x => x.AdminMail == Mail && x.AdminİsimKullanıcıAd == Name).FirstOrDefault();
            if (dgr2!=null && Mail!="")
            {
                Random rastgele = new Random();
                string harfler = "ABCDEFGHIJKLMNOPRSTUVYZWX";
                _OnayKodu = "";
                for (int i = 0; i < 6; i++)
                {
                    _OnayKodu += harfler[rastgele.Next(harfler.Length)];
                }
                var dgr = c.Admins.Where(x => x.IsDeleted == false && x.AdminMail == Mail).FirstOrDefault();
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(_OnayKodu);
                dgr.Sifre = encrypedPassword;
                dgr.Salt = crypto.Salt;
                c.SaveChanges();
                MailMessage mailim = new MailMessage();
                mailim.To.Add(Mail);
                mailim.From = new MailAddress("gdelice2244@gmail.com");
                mailim.Subject = "Sisteme Giriş İçin Tek kullanımlık Şifreniz";
                mailim.IsBodyHtml = true;
                mailim.Body = "Sayın yetkili, " + "<b>" + Mail + " " + "</b>" + "mail hesabı ile açtığınız hesaba erişim için tek kullanımlık şifreniz:" + "<b>" + _OnayKodu + "</b>" + "." + "Bu şifre ile sisteme giriş yaparak şifrenizi yenileyebilirsiniz.";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("gdelice2244@gmail.com", "aqyebzvykbchfffl");
                try
                {
                    smtp.Send(mailim);
                    TempData["SendPassword"] = "Mail Adresinize Tek Kullanımlık Şifreniz İletilmiştir.";
                }
                catch (Exception ex)
                {
                    TempData["SendPassword"] = "Mail Adresine Şifre Gönderilemedi.Hata nedeni:" + ex.Message;
                }
                return RedirectToAction("Password", "Login");
            }
            else
            {
                if (Mail == "") {
                    TempData["SendPassword"] = "Lütfen  Mail Adresi Giriniz";
                    return RedirectToAction("Password", "Login");
                }
                else 
                {
                    TempData["SendPassword"] = "Lütfen Kendi Hesabınızın Mail Adresini ve kullanıcı Adını Giriniz";
                    return RedirectToAction("Password", "Login");
                }
                
            }
        }


        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            var dgr = c.Admins.Where(x => x.AdminMail == admin.AdminMail).ToList();
            if (dgr == null)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(admin.Sifre);
                admin.Sifre = encrypedPassword;
                admin.Salt = crypto.Salt;
                c.Admins.Add(admin);
                c.SaveChanges();
                TempData["AlertMessage"] = "Admin Bilgileri Başarı İle Eklendi";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["AlertMessage"] = "Bu Mail Hesabı İle oluşturulmuş bir kullanıcı zaten var,lütfen başka bir Mail hesabı girin.";
                return RedirectToAction("Index", "Admin");
            }
        }
        public ActionResult RemoveAdmin(int id)
        {
            string session = Session["AdminİsimKullanıcıAd"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.AdminİsimKullanıcıAd == session).FirstOrDefault();
            if (id == dgr.ID)
            {
                TempData["Warning"] = "Kendi Admin Bilgilerinizi SİLEMEZSİNİZ";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var b = c.Admins.Find(id);
                b.IsDeleted = true;
                c.SaveChanges();
                if (b.IsDeleted == true)
                {
                    TempData["AlertMessage"] = "Admin Bilgileri Başarı İle Silindi";
                }
                return RedirectToAction("Index", "Admin");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult UpdateMyProfile(Admin admin)
        {
            var b = c.Admins.Find(admin.ID);
            b.ID = admin.ID;
            b.Adminİsim = admin.Adminİsim;
            b.AdminMail = admin.AdminMail;
            b.AdminİsimKullanıcıAd = admin.AdminİsimKullanıcıAd;
            b.AdmiTelefon = admin.AdmiTelefon;

            if (b.Sifre != admin.Sifre)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(admin.Sifre);
                b.Sifre = encrypedPassword;
                b.Salt = crypto.Salt;
            }
            c.SaveChanges();
            return RedirectToAction("MyProfile");

        }
        [Authorize]
        public PartialViewResult Admin_Layout()
        {
            string session = Session["AdminİsimKullanıcıAd"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.AdminİsimKullanıcıAd == session).Take(1).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Notifications()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.control==false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Notifications2()
        {
           
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.control == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Notifications3()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == false && x.control == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Admin_Welcome()
        {
            string session = Session["AdminİsimKullanıcıAd"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.AdminİsimKullanıcıAd == session).Take(1).ToList();
            return PartialView(dgr);
        }
    }
}