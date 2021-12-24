using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaoVat.Areas.Admin.Controllers
{
    public class AdminNewsController : Controller
    {
        RaoVatModel db = new RaoVatModel();
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        // GET: Admin/AdminNews
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(db.News.Where(x=>x.Status==0).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Detail(string IDNews)
        {
            return View(db.News.Where(x => x.IDNews == IDNews).FirstOrDefault());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ImgNews(string IDNews)
        {
            List<ImgNews> list = db.ImgNews.Where(x => x.IDNews == IDNews).ToList();
            foreach(var item in list)
            {
                item.Image =item.ImgURL;
            }
            return PartialView(list);
        }
        public JsonResult Agree(string IDNews)
        {
            News news = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
            news.Status = 2;
            db.Entry(news).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            string msg = "Tin đã được duyệt";
            return Json(msg,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Denine(string IDNews)
        {
            News news = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
            news.Status = 1;
            db.Entry(news).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            string msg = "Tin đã bị từ chối";
            return Json(msg,JsonRequestBehavior.AllowGet);
        }
    }
}