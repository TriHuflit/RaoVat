using RaoVat.Areas.Admin.Models;
using RaoVat.Common;
using RaoVat.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RaoVat.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Index(LoginModel model)
        {
            
            //var result = new AccountDAO().Login(model.UserName, model.PassWord);
            if(Membership.ValidateUser(model.UserName,model.PassWord) && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName,false);             
                return RedirectToAction("Index", "Admin");
           
            }
            else
            {
                ModelState.AddModelError("", "-Tên tài khoản hoặc mật khẩu không đúng");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}