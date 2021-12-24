using RaoVat.Common;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaoVat.Controllers
{
    public class NewsController : Controller
    {
        RaoVatModel db = new RaoVatModel();
        // GET: News
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InfoNews(string IDNews)
        {
            if (IDNews != null)
            {
                News news = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
                Users user = db.Users.Where(x => x.IDUser == news.IDUser).FirstOrDefault();
                if (user.Avatar != null)
                {
                    news.Users.Image = ConvertImage(user.Avatar);
                }
                else
                {
                    news.Users.Image = "/Asset/img/user.png";
                }
                return View(news);
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult ListNewsSame(News model)
        {
            if (model.IDNews != null)
            {
                IEnumerable<News> news = db.News.Where(x => x.Brand.IDSubCategory == model.Brand.IDSubCategory && x.IDNews != model.IDNews).ToList();
                if (news.ToList().Count > 5)
                {
                    news = news.Take(5);
                }
                foreach (var item in news)
                {
                    if (item.Users.Avatar != null)
                    {
                        item.Users.Image = ConvertImage(item.Users.Avatar);
                    }
                    else
                    {
                        item.Users.Image = "./Asset/img/user.png";
                    }
                    ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                    item.Image = imgNews.ImgURL;
                }
                return PartialView(news);
            }
            return RedirectToAction("Index", "Home");
        }
        [ChildActionOnly]
        public ActionResult ImgNews(string IDNews)
        {
            List<ImgNews> imgNews = db.ImgNews.Where(x => x.IDNews == IDNews).ToList();
            foreach(var item in imgNews)
            {
                item.Image = item.ImgURL;
            }
            return PartialView(imgNews);
        }
        [ChildActionOnly]
        [HttpGet]
        public ActionResult NewsOfOtherUser(string IDUser)
        {


            List<News> news = db.News.Where(x => x.IDUser == IDUser && x.Status == 2).ToList();
            foreach (var item in news)
            {
                ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                item.Image = imgNews.ImgURL;
            }
            return PartialView(news);
        }
        [HttpGet]

        public ActionResult newsOfUser()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
                List<News> news = db.News.Where(x => x.IDUser == User.User_ID && x.Status == 2).ToList();
                foreach (var item in news)
                {
                    ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                    item.Image = imgNews.ImgURL;
                }
                return PartialView(news);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]

        public ActionResult newsRefuse()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
                List<News> news = db.News.Where(x => x.IDUser == User.User_ID && x.Status == 1).ToList();
                foreach (var item in news)
                {
                    ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                    item.Image = imgNews.ImgURL;
                }
                return PartialView(news);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LoadNewsBySearch(List<News> news,double min,double max,string IDCategory,string IDSubCategory,string IDBrand)
        {
            if (news == null && IDCategory==null && IDSubCategory == null && IDBrand==null )
            {
                news = db.News.Where(x => x.Price >= min && x.Price <= max && x.Status==2).ToList();
              
            }else if (news == null && IDCategory != null)
            {
                news = db.News.Where(x =>x.Price >= min && x.Price <= max && x.Brand.SubCategory.IDCategory==IDCategory && x.Status == 2).ToList();
            }
            else if (news == null && IDBrand != null)
            {
                news = db.News.Where(x => x.Price >= min && x.Price <= max && x.Brand.Name == IDBrand && x.Status == 2).ToList();
            }
            else 
            {
                news = db.News.Where(x => (x.Price >= min && x.Price <= max) && x.Brand.IDSubCategory == IDSubCategory && x.Status == 2).ToList();
            }        
            foreach (var item in news)
            {
                if (item.Users.Avatar != null)
                {
                    item.Users.Image = ConvertImage(item.Users.Avatar);
                }
                else
                {
                    item.Users.Image = "./Asset/img/user.png";
                }
                ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                item.Image = imgNews.ImgURL;
            }
            return PartialView(news);
        }
        public ActionResult LoadSubCate(string IDCategory)
        {
            List<SubCategory> model = db.SubCategory.Where(x => x.IDCategory == IDCategory).ToList();
            foreach (var item in model)
            {
                item.Image = ConvertImage(item.ImageSubCate);
            }
            return PartialView(model);
        }
        public ActionResult LoadBrand(string IDSubCategory)
        {
            List<Brand> model = db.Brand.Where(x => x.IDSubCategory == IDSubCategory).ToList();
            foreach (var item in model)
            {
                item.Image = ConvertImage(item.ImageBrand);
            }
            return PartialView(model);
        }
    }
}