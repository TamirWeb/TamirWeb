using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Tamirci.Models;
using Tamirci.ViewModel;
using PagedList;

namespace Tamirci.Controllers
{
    public class TamircilerController : Controller
    {
        // GET: Tamirciler
        Context c = new Context();
        public void Calculate()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).ToList();
            int count = 0;
            int count1 = 0;
            int count2 = 0;
            foreach (var k in dgr)
            {
                count = c.Puans.Where(x => x.Tamirciid == k.ID).Count();
                count1 = c.Puans.Where(x => x.Durum == true && x.Tamirciid==k.ID).Count();
                count2 = c.Puans.Where(x => x.Durum == false && x.Tamirciid == k.ID).Count();
                if (count != 0)
                {
                    k.Tamirci_Puan = ((count1 * 6) - (count2 * 2)) / count;
                }

               else
                {
                    k.Tamirci_Puan = 0;
                }
                count1 = 0;
                count2 = 0;
                count = 0;
                c.SaveChanges();
            }
        }
        public ActionResult Index(int sayfa=1)
        {
            Calculate();
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==true).ToList().ToPagedList(sayfa,8);
            return View(dgr);
        }
        public ActionResult Oto(int sayfa = 1)
        {
            Calculate();
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.Kategoriid==1).ToList().ToPagedList(sayfa, 8); 
            return View(dgr);
        }
        public ActionResult Filtre(bool check1, bool check2, bool check3, bool check4, bool check5, bool check6, bool check7, bool check8,İlçeViewModel model)
        {
            var dgr=c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).ToList();
            var dgr1 = c.İls.Find(model.ilid);
            var dgr2 = c.İlçes.Find(model.ilçeid);
            if (dgr1 != null && dgr2 == null)
            {
                dgr = dgr.Where(x => x.Tamirci_İl == dgr1.Name).ToList();
            }
            if(dgr1!=null && dgr2!=null)
            {
                dgr = dgr.Where(x => x.Tamirci_İl == dgr1.Name).ToList();
                dgr=dgr.Where(x => x.Tamirci_İlçe == dgr2.Name).ToList();
            }
            Calculate();
            if (check1)
            {
               dgr =dgr.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.Kategoriid == 1).ToList();
            }
            if(check2)
            {
                 dgr = dgr.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.Kategoriid == 2).ToList();
            }
            if (check3)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).OrderByDescending(x=>x.Click).ToList();
            }
            if (check4)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).OrderBy(x=>x.Click).ToList();
            }
            if (check5)
            {
                dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).OrderByDescending(x => x.Yorumlars.Count).ToList();
            }
            if (check6)
            {
                dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true ).OrderBy(x => x.Yorumlars.Count).ToList();
            }
            if (check7)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).OrderByDescending(x => x.Tamirci_Puan).ToList();
            }
            if (check8)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true ).OrderBy(x => x.Tamirci_Puan).ToList();
            }
            return View(dgr);
        }
        public ActionResult Motor(int sayfa = 1)
        {
            Calculate();
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.Kategoriid == 2).ToList().ToPagedList(sayfa, 8); 
            return View(dgr);
        }
        public ActionResult TamirciDetail(int id)
        {
            var dgr = c.Tamircilers.Where(x => x.ID == id).ToList();
            var dgr2 = dgr.FirstOrDefault();
            dgr2.Click = dgr2.Click + 1;
            c.SaveChanges();
            return View(dgr);
        }
        public PartialViewResult SonEklenenYorum()
        {
            var dgr = c.Yorumlars.Where(x => x.IsDeleted == false && x.Aktiflik == true).OrderByDescending(x => x.ID).Take(5).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult SonMesajlar()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.control == true).OrderByDescending(x => x.ID).Take(5).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult SonEklenenTamirci()
        {
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true).OrderByDescending(x => x.ID).Take(5).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult Section1(int id)
        {
            var deger = c.Yorumlars.Where(x => x.Tamirci.ID == id && x.IsDeleted == false && x.control == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(deger);
        }
        [HttpGet]
        public PartialViewResult Yorumekle(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Yorumekle(Yorumlar a)
        {
            if (a.Yorumİcerik != null)
            {
                a.Tamirciid = a.Tamirciid;
                a.control = false;
                a.IsDeleted = false;
                a.Aktiflik = false;
                c.Yorumlars.Add(a);
                c.SaveChanges();
            }
            else
            {

            }
            return PartialView();
        }
        [HttpGet]
        public ActionResult TamirciBaşvuru()
        {
            KategoriViewModel model = new KategoriViewModel();
            List<Kategori> stkurumlistesi = c.Kategoris.Where(x => x.IsDeleted == false).ToList();
            model.Kategorilist = (List<SelectListItem>)(from s in stkurumlistesi
                                                        select new SelectListItem
                                                        {
                                                            Text = s.kategori,
                                                            Value = s.Id.ToString()
                                                        }).ToList();
            model.Kategorilist.Insert(0, new SelectListItem { Text = "Seçiniz", Value = " ", Selected = true });
            return View(model);
        }
        [HttpPost]
        public ActionResult TamirciBaşvuru(Tamirciler a, KategoriViewModel b, İlçeViewModel d)
        {
            a.Kategoriid = b.Kategoriid;
            var dgr1 = c.İls.Find(d.ilid);
            a.Tamirci_İl = dgr1.Name.ToUpper();
            var dgr2 = c.İlçes.Find(d.ilçeid);
            a.Tamirci_İlçe = dgr2.Name.ToUpper();
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
            a.TamirciAdı = a.TamirciAdı.ToUpper();
            a.Tamirci_Aktiflik = false;
            a.control = false;
            a.Tamirci_Puan = 0;
            a.Click = 0;
            c.Tamircilers.Add(a);
            c.SaveChanges();
            TempData["MesajGonderildi"] = "Başvurunuz bize ulaştı.Uygun görülmesi durumunda Firmanız sayfamızda yer alacaktır.";
            MailMessage mailim = new MailMessage();
            string Mail = a.TamirciMail;
            mailim.To.Add(Mail);
            mailim.From = new MailAddress("gdelice2244@gmail.com");
            mailim.Subject = "Yeni bir Durum Güncellemeniz Var.";
            mailim.IsBodyHtml = true;
            mailim.Body = "Sayın yetkili, " + "<b>" + a.TamirciAdı + " " + "</b>" + "adlı firma başvuru talebiniz alınmıştır.Uygun görülmesi halinde Firmanız sayfamızda yer alacaktır.";
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
            return RedirectToAction("TamirciBaşvuru", "Tamirciler");
        }

        public PartialViewResult İlİlçe()
        {
            İlçeViewModel model = new İlçeViewModel();
            List<İl> illiste = c.İls.Where(x => x.IsDeleted == false).ToList();
            model.illist = (List<SelectListItem>)(from s in illiste
                                                        select new SelectListItem
                                                        {
                                                            Text = s.Name,
                                                            Value = s.ID.ToString()
                                                        }).ToList();
      
            model.illist.Insert(0, new SelectListItem { Text = "Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }
        public JsonResult GetInstitution(int id)
        {
            List<İlçe> ilçelist = c.İlçes.Where(x => x.İlid == id && x.IsDeleted==false).ToList();
            List<SelectListItem> dgr = (from i in c.İlçes.Where(x => x.İlid== id && x.IsDeleted==false)
                                        select new SelectListItem
                                        {
                                            Text = i.Name,
                                            Value = i.ID.ToString()
                                        }).ToList();
            return Json(dgr, JsonRequestBehavior.AllowGet);
        }
       
       

       
    }
}