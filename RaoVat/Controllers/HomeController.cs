using RaoVat.Common;
using RaoVat.DAO;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Text.RegularExpressions;
using System.Text;
using RaoVat.DesignPattern;

namespace RaoVat.Controllers
{
    public class HomeController : Controller
    {
        public RaoVatModel db = new RaoVatModel();
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadNews(int page=1,int pageSize=10)
        {
            IEnumerable<News> news;
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                UserSession user =(UserSession)Session[CommonConstants.USER_SESSION];
                news = db.News.Where(x => x.Status == 2 && x.IDUser!=user.User_ID).OrderByDescending(x => x.Time.Value).ToPagedList(page,pageSize);
            }
            else
            {
                news = db.News.Where(x => x.Status == 2).OrderByDescending(x => x.Time).ToPagedList(page,pageSize);          
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
      
        [ChildActionOnly]
        public ActionResult WishList(string IDNews)
        {
            BoxSave boxsave = new BoxSave();
            boxsave.IDNewsCurrent = IDNews;
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                UserSession user = (UserSession)Session[CommonConstants.USER_SESSION];
                boxsave = db.BoxSave.Where(x => x.IDUser == user.User_ID && x.IDNews == IDNews).FirstOrDefault();
                if (boxsave == null)
                    boxsave = new BoxSave();
                    boxsave.IDNewsCurrent = IDNews;
                return PartialView(boxsave);
            }
            return PartialView(boxsave);
        }
        [ChildActionOnly]
        public ActionResult LoadCategory()
        {
            IEnumerable<Category> listcategory = db.Category.ToList();
            foreach (var item in listcategory)
            {
                if (item.ImageCate != null)
                {
                    item.Image = ConvertImage(item.ImageCate);
                }
            }
            return PartialView(listcategory);
        }
        [ChildActionOnly]
        public ActionResult CheckLogged()
        {
            return PartialView("_CheckLogged");
        }
        [HttpPost]
        public JsonResult AddWishList(string IDNews)
        {
            BoxSave boxsave = new BoxSave();
            string result = "";
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                
                UserSession user = (UserSession)Session[CommonConstants.USER_SESSION];
                boxsave=db.BoxSave.Where(x => x.IDUser == user.User_ID && x.IDNews == IDNews).FirstOrDefault();
                if (boxsave == null)
                {
                    boxsave = new BoxSave();
                    boxsave.IDBoxSave= db.Database.SqlQuery<string>("select dbo.fn_getRandom_ValueImg()").FirstOrDefault();
                    boxsave.IDNews = IDNews;
                    boxsave.IDUser = user.User_ID;
                    db.BoxSave.Add(boxsave);
                    db.SaveChanges();
                    result = "True";
                }
                else
                {
                    db.BoxSave.Remove(boxsave);
                    db.SaveChanges();
                    result = "False";
                }
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
           
        }
        [ChildActionOnly]
       
        public ActionResult Search()
        {
            return PartialView();
        }
        public JsonResult ListNameBySearch(string q)
        {
            IEnumerable<string> data = db.News.Where(x =>  x.Name.Contains(q) && x.Status == 2).Select(x => x.Name).ToList();
            if (data.ToList().Count > 10)
            {
               data= data.Take(10);
            }
            return Json(new
           {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet) ;
        }
        [HttpGet]
        public ActionResult searchByKeys()
        {
            ViewBag.ListCate = db.Category.ToList();
            return View();
        }
        public string RemoveUnicode(string s)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = s.Normalize(NormalizationForm.FormD);
            string key= regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return key;
        }
        [HttpPost]
        public ActionResult searchByKeys(string keyword)
        {  
          
            keyword = RemoveUnicode(keyword);
            List<News> news= db.News.Where( delegate (News c)
            {
                if (RemoveUnicode(c.Name.ToLower()).IndexOf(keyword.ToLower()) >= 0 && c.Status == 2)
                    return true;
                else if (RemoveUnicode(c.Brand.Name.ToLower()).IndexOf(keyword.ToLower()) >= 0 && c.Status == 2)
                    return true;
                else if (RemoveUnicode(c.Brand.SubCategory.Name.ToLower()).IndexOf(keyword.ToLower()) >= 0 && c.Status == 2)
                    return true;
                else if (RemoveUnicode(c.Brand.SubCategory.Category.Name.ToLower()).IndexOf(keyword.ToLower()) >= 0 && c.Status == 2)
                    return true;
                else
                    return false;
            }).ToList();
            //List<News> model = db.News.Where(x => x.Name.ToLower().Contains(keyword.ToLower()) && x.Status==2).OrderBy(x => x.Time).ToList();
            IIterator iterator = new NewsIterator(news);
            var item = iterator.First();
            while (!iterator.isDone)
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
                item = iterator.Next();
            }
            ViewBag.ListCate = db.Category.ToList();
            return View(news);
        }
        public ActionResult searchByCategory(string IDCategory)
        {
            if (IDCategory != null)
            {
                ViewBag.ListCate = db.Category.ToList();
                ViewBag.Category = IDCategory;
                List<News> news = db.News.Where(x => x.Brand.SubCategory.Category.IDCategory == IDCategory && x.Status == 2).ToList();
                //foreach (var item in news)
                //{
                //    if (item.Users.Avatar != null)
                //    {
                //        item.Users.Image = ConvertImage(item.Users.Avatar);
                //    }
                //    else
                //    {
                //        item.Users.Image = "./Asset/img/user.png";
                //    }
                //    ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                //    item.Image = imgNews.ImgURL;
                //}
                IIterator iterator = new NewsIterator(news);
                var item = iterator.First();
                while (!iterator.isDone)
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
                    item = iterator.Next();
                }
                return View(news);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult searchBySubCategory(string IDSubCategory)
        {
            if (IDSubCategory != null)
            {
                ViewBag.ListCate = db.Category.ToList();
                ViewBag.SubCategory = IDSubCategory;
                List<News> news = db.News.Where(x => x.Brand.SubCategory.IDSubCategory == IDSubCategory && x.Status == 2).ToList();
                //foreach (var item in news)
                //{
                //    if (item.Users.Avatar != null)
                //    {
                //        item.Users.Image = ConvertImage(item.Users.Avatar);
                //    }
                //    else
                //    {
                //        item.Users.Image = "./Asset/img/user.png";
                //    }
                //    ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                //    item.Image = imgNews.ImgURL;
                //}
                IIterator iterator = new NewsIterator(news);
                var item = iterator.First();
                while (!iterator.isDone)
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
                    item = iterator.Next();
                }
                return View(news);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}