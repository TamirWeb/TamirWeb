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
        public ActionResult Index()
        {
            Calculate();
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik==true).ToList();
            return View(dgr);
        }
        public ActionResult Oto()
        {
            Calculate();
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.Kategoriid==1).ToList();
            return View(dgr);
        }
        public ActionResult Motor()
        {
            Calculate();
            var dgr = c.Tamircilers.Where(x => x.IsDeleted == false && x.Tamirci_Aktiflik == true && x.Kategoriid == 2).ToList();
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
        [HttpGet]
        public ActionResult TamirciBaşvuru()
        {
            KategoriViewModel model = new KategoriViewModel();
            List<Kategori> stkurumlistesi = c.Kategoris.ToList();
            model.Kategorilist = (List<SelectListItem>)(from s in stkurumlistesi
                                                        select new SelectListItem
                                                        {
                                                            Text = s.kategori,
                                                            Value = s.Id.ToString()
                                                        }).ToList();
            model.Kategorilist.Insert(0, new SelectListItem { Text = "Seçiniz", Value = " ", Selected = true });
            return View(model);

        }
        public PartialViewResult İlİlçe()
        {
            İlçeViewModel model = new İlçeViewModel();
            List<İl> illiste = c.İls.ToList();
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
            List<İlçe> ilçelist = c.İlçes.Where(x => x.İlid == id).ToList();
            List<SelectListItem> dgr = (from i in c.İlçes.Where(x => x.İlid== id)
                                        select new SelectListItem
                                        {
                                            Text = i.Name,
                                            Value = i.ID.ToString()
                                        }).ToList();
            //var dgr = (from x in c.Institutions
            //           join y in c.Parentinstitutions on x.Parentinstitution.Id equals y.Id
            //           where x.Parentinstitution.Id == p
            //           select new
            //           {
            //               Text = x.Name,
            //               value = x.Id.ToString()
            //           }).ToList();
            return Json(dgr, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TamirciBaşvuru(Tamirciler a, KategoriViewModel b,İlçeViewModel d)
        {
            a.Kategoriid = b.Kategoriid;
            var dgr1 = c.İls.Find(d.ilid);
            a.Tamirci_İl = dgr1.Name;
            var dgr2 = c.İlçes.Find(d.ilçeid);
            a.Tamirci_İlçe = dgr2.Name;
            if (Request.Files.Count > 0  && Request.Files[0].ContentLength > 0)
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




            return RedirectToAction("TamirciBaşvuru","Tamirciler");
        }
        public PartialViewResult Section1(int id)
        {
          var deger = c.Yorumlars.Where(x => x.Tamirci.ID == id).OrderByDescending(x => x.ID).ToList();
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

       
    }
}