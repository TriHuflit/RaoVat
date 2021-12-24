using RaoVat.Common;
using RaoVat.DAO;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Firebase.Auth;
using System.Threading;
using Firebase.Storage;
using RaoVat.Context;

namespace RaoVat.Controllers
{
    [HandleError]
    public class UserProfileController : Controller
    {
        RaoVatModel db = new RaoVatModel();

        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        // GET: UserProfile
        [HttpGet]
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
                Users user = db.Users.Where(x => x.IDUser == User.User_ID).FirstOrDefault();
                if (user.Avatar == null)
                {
                    user.Image = "./Asset/img/user.png";
                }
                else
                {
                    user.Image = ConvertImage(user.Avatar);
                }
                return View(user);
            }

            return RedirectToAction("Index","UserLogin");
        }
        [HttpGet]
        public ActionResult InfoOtherUser(string IDNews)
        {
            if(IDNews!=null)
            {
                var news = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
                Users user = db.Users.Where(x => x.IDUser == news.IDUser).FirstOrDefault();
                if (user.Avatar == null)
                {
                    user.Image = "./Asset/img/user.png";
                }
                else
                {
                    user.Image = ConvertImage(user.Avatar);
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
       
        }
      
        [HttpGet]
        public ActionResult Editinfo()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                
                    UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
                    Users user = db.Users.Where(x => x.IDUser == User.User_ID).FirstOrDefault();
                    if (user.Avatar == null)
                    {
                        user.Image = "~/Asset/img/user.png";
                    }
                    else
                    {
                        user.Image = ConvertImage(user.Avatar);
                    }
                    user.PassWord = new AccountDAO().ToHex(user.PassWord);
                    return View(user);
                
               
            }

            return RedirectToAction("Index", "UserLogin");
        }
        [HttpGet]
        public ActionResult Editnews(string IDNews)
        {
            if (Session[CommonConstants.USER_SESSION] != null || IDNews != null)
            {
                News model = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
                var brand = db.Brand.Where(x => x.IDBrand == model.IDBrand).FirstOrDefault();

                model.nameBrand =  brand.SubCategory.Name + " " +brand.Name;
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult EditnewsRefuse(string IDNews)
        {
            if (Session[CommonConstants.USER_SESSION] != null || IDNews == null)
            {
                News model = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
                model.listCate = db.Category.ToList();
                TempData["listCate-edit"] = model.listCate;
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        public  ActionResult EditImgNews(string IDNews)
        {
            List<ImgNews> list = db.ImgNews.Where(x => x.IDNews == IDNews).ToList();
            foreach (var item in list)
            {
                item.Image =  item.ImgURL;
            }
            return PartialView(list);
        }
        [HttpGet]
        public ActionResult createOfNews()
        {
            string msg = "";
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
                Users user = db.Users.Where(x => x.IDUser == User.User_ID).FirstOrDefault();
                List<News> listnews = db.News.Where(x => x.IDUser == user.IDUser).ToList();
              
                if (listnews.Count < 3)
                {
                    News news = new News();
                    news.listCate = db.Category.ToList();
                    TempData["listCate"] = news.listCate;
                    return View(news);
                }
                else
                {
                    msg = "True";
                    return Json(msg,JsonRequestBehavior.AllowGet);
                }
            }
            msg = "False";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult BoxSave(string IDUser)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var boxsave = db.BoxSave.Where(x => x.IDUser == IDUser).ToList();
                foreach (var item in boxsave)
                {
                    ImgNews imgNews = db.ImgNews.Where(x => x.IDNews == item.IDNews).FirstOrDefault();
                    item.Image = imgNews.ImgURL;
                }
                return View(boxsave);
            }
            return RedirectToAction("Index", "UserLogin");
        }
        [HttpPost]
        public  async Task<ActionResult> Editnews(HttpPostedFileBase[] Images, News model)
        {
            if (ModelState.IsValid)
            {
                var listImg = db.ImgNews.Where(x => x.IDNews == model.IDNews).ToList();
               
                if (model.listImage.Count > 0)
                {
                    foreach (var item in listImg)
                    {
                        bool temp = false;
                        foreach (var img in model.listImage)
                        {
                            if (!img.Contains("blob:") && item.ImgURL == img)
                            {
                                temp = true;
                            }
                        }
                        if (temp == false)
                        {
                            db.ImgNews.Remove(item);
                            db.SaveChanges();
                            await Task.Run(() => ConnectFireBase.Delete(item.NameImage));
                        }
                    }
                }
               
                if (Images.Length > 0 && Images[0] != null)
                {
                    foreach (var item in Images)
                    {
                        FileStream steam;
                        if (item.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/Content/images/"), item.FileName);
                            item.SaveAs(path);
                            steam = new FileStream(Path.Combine(path), FileMode.Open);
                            Task.WaitAll(Task.Run(()=>ConnectFireBase.Upload(steam, item.FileName, model)));


                        }
                    }
                }
                NewsDAO Editnews = new NewsDAO();
                Editnews.EditNews(model);                         
                ViewBag.status = "True";
                return View(model);
            }
            ViewBag.status = "False";
            return View(model);
        }
    
        [HttpPost]
        public ActionResult EditnewsRefuse(HttpPostedFileBase[] Images, News model)
        {
            if (ModelState.IsValid)
            {
                NewsDAO Editnews = new NewsDAO();
                Editnews.EditnewsRefuse(Images, model);
                model.listCate = db.Category.ToList();
                ViewBag.status = "True";
                return View(model);
            }
            model.listCate = (List<Category>)TempData["listCate-edit"];
            if (Images == null && model.listImage.Count==0)
            {
                ViewBag.ErrorImg = "False";
            }
            ViewBag.status = "False";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> createOfNews(HttpPostedFileBase[] Image, News news)
        {
            if(Image != null)
            {
                if (ModelState.IsValid)
                {
                   
                    UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
                    news.IDUser = User.User_ID;
                    news.IDNews = db.Database.SqlQuery<string>("select dbo.fn_getRandom_Value()").FirstOrDefault();
                    news.Time = DateTime.Now;
                    news.Status = 0;
                    db.News.Add(news);
                    db.SaveChanges();
                    
                    foreach (var item in Image)
                    {

                        FileStream steam;
                        if (item.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/Content/images/"), item.FileName);
                            item.SaveAs(path);
                            steam = new FileStream(Path.Combine(path), FileMode.Open);
                            await Task.Run(() => ConnectFireBase.Upload(steam, item.FileName, news));
                        }
                    }
                    news = new News();
                    news.listCate = db.Category.ToList();               
                    ViewBag.status = "True";
                    ModelState.Clear();
                    return View(news);
                }

            }          
            news.listCate = (List<Category>)TempData["listCate"];
            ViewBag.ErrorImg = "False";
            ViewBag.status = "False";
            return View(news);
        }
   
        public ActionResult GetSubCategory(string IDCategory)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> SubCateList = db.SubCategory.Where(x => x.IDCategory == IDCategory).ToList();
            ViewBag.SubCateList = new SelectList(SubCateList, "IDSubCategory", "Name");
            return PartialView("SubCateOption");

        }
        public ActionResult GetBrand(string IDSubCategory)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Brand> brandList = db.Brand.Where(x => x.IDSubCategory == IDSubCategory).ToList();
            ViewBag.BrandList = new SelectList(brandList, "IDBrand", "Name");
            return PartialView("BrandOption");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editinfo(HttpPostedFileBase Image,Users model)
        {
            UserSession User = (UserSession)Session[CommonConstants.USER_SESSION];
            Users user = db.Users.Where(x => x.IDUser == User.User_ID).FirstOrDefault();
            if (Image != null)
            {
                model.Avatar = new byte[Image.ContentLength];
                Image.InputStream.Read(model.Avatar, 0, Image.ContentLength);
            }
            else
            {
                 model.Image = "~/Asset/img/user.png";
            }
            var email = db.Users.Where(x => x.Email == model.Email && user.IDUser != x.IDUser).FirstOrDefault();
            if (email == null)
            {
                user = new UserDAO().ChangeInfo(model, user);
                ModelState["UserName"].Errors.Clear();
                if (ModelState.IsValid)
                {
                  
                    ProxyUser proxyUser = new ProxyUser(user);
                    string checkUsers = proxyUser.UpdateProfile(db);
                    if(checkUsers== "Ivalid FullName")
                    {
                        ViewBag.status = "False";
                        ViewBag.FullName = "False";
                        if (model.Avatar != null)
                        {
                            User.Avatar = ConvertImage(model.Avatar);
                        }
                        
                        return View(user);
                    }
                    else
                    {
                       
                        ViewBag.status = "True";
                        if (model.Avatar != null)
                        {
                            User.Avatar = ConvertImage(model.Avatar);
                        }
                        User.User_Name = model.FullName;
                        return View(user);
                    }
                  
                }
                else
                {
                    ViewBag.status = "False";
                    return View(model);
                }
            }
            else
            {
                ViewBag.status = "False";
                ViewBag.EmailError = "False";
                return View(model);
            }
         
        }
        public JsonResult Delete(string IDNews)
        {

            var ImageNews = db.ImgNews.Where(x => x.IDNews == IDNews).ToList();
            db.ImgNews.RemoveRange(ImageNews);
            db.SaveChanges();
            string msg = "";
            var news = db.News.Where(x => x.IDNews == IDNews).FirstOrDefault();
            if (news.Status == 2)
            {
                msg = "True";
            }
            else
            {
                msg = "False";
            }
            db.News.Remove(news);
            db.SaveChanges();
            return Json(msg,JsonRequestBehavior.AllowGet);
        }

    }
}