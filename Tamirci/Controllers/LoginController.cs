using Tamirci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Tamirci.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin a)
        {
            var cyrpto = new SimpleCrypto.PBKDF2();
            var user = c.Admins.Where(x => x.AdminİsimKullanıcıAd == a.AdminİsimKullanıcıAd).FirstOrDefault();
            //var dgr = c.Admins.FirstOrDefault(x => x.UserName == a.UserName && x.Password == a.Password);
            if (user != null)
            {
                if (user.Sifre == cyrpto.Compute(a.Sifre, user.Salt))
                {
                    FormsAuthentication.SetAuthCookie(user.AdminİsimKullanıcıAd, false);
                    Session["AdminİsimKullanıcıAd"] = user.AdminİsimKullanıcıAd.ToString();
                    return RedirectToAction("MyProfile", "Login");
                  
                }
                else
                {
                    TempData["WrongLogin"] = "Kullanıcı Adı Veya Şifreniz Hatalı!!";
                    return RedirectToAction("Login", "Login");
                }

            }
            else
            {
                return View();
            }

        }
        [Authorize]
        public ActionResult MyProfile()
        {
            string session = Session["AdminİsimKullanıcıAd"].ToString();
            var dgr = c.Admins.Where(x => x.AdminİsimKullanıcıAd == session).Take(1).ToList();
            return View(dgr);
        }
        [Authorize]
        [Authorize]
        public ActionResult GetMyProfile(int id)
        {
            var deger = c.Admins.Find(id);
            return View(deger);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var encrypedPassword = crypto.Compute(admin.Sifre);
            admin.Sifre = encrypedPassword;
            admin.Salt = crypto.Salt;
            c.Admins.Add(admin);
            c.SaveChanges();
            return RedirectToAction("MyProfile");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        [Authorize]
        public PartialViewResult Admin_Layout()
        {
            string session = Session["AdminİsimKullanıcıAd"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.AdminİsimKullanıcıAd == session).Take(1).ToList();
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