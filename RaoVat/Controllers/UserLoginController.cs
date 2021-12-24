using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using RaoVat.Common;
using RaoVat.DAO;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;

namespace RaoVat.Controllers
{
    public class UserLoginController : Controller
    {
        public RaoVatModel db = new RaoVatModel();
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        // GET: UserLogin
        [HttpGet]
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AccountLogin model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.PassWord))
                {
                      
                    var userSession = new UserSession();
                    var infoUser = new AccountDAO().GetInfoByUserName(model.UserName);
                    if (infoUser.EmailComfirm)
                    {
                        userSession.User_ID = infoUser.IDUser;
                        userSession.User_Name = infoUser.FullName;
                        if (infoUser.Avatar != null)
                        {
                            userSession.Avatar = ConvertImage(infoUser.Avatar);
                        }
                      
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
                    }
                    else ViewData["Error"] = "Tài khoản của bạn chưa được xác minh,vui lòng thử lại sau !!!";
                }
                else
                {
                    ViewData["Error"]= "Tài khoản hoặc mật khẩu không đúng";
                }
            }       
            return View(model);
        }
        [HttpPost]
       
        public ActionResult Register(Register model)
        {
         
            if (ModelState.IsValid)
            {
                string res = new AccountDAO().CheckRegister(model.UserName, model.PassWord, model.Email);
                if(res=="")
                {
                    TempData["Success"] = "Đăng ký thành công,vui lòng vào Email: " +model.Email+" của bạn để xác nhận tài khoản !";
                    BuildEmailTemplate(model.UserName);
                }
                else
                {
                    ViewData["Error"] = res;
                }
                return View(model);
            }
            return View(model);
        }
       

        public ActionResult Confirm(string IDUser)
        {
            ViewBag.IDUser = IDUser;
            return View();
        }
        public JsonResult RegisterConfirm(string IDUser)
        {
            var Data = db.Users.Where(x => x.IDUser == IDUser).FirstOrDefault();
            if (Data != null)
            {
                Data.EmailComfirm = true;
                Data.Address = "Chưa cập nhật";
                Data.Phone = "0900000000";
                db.SaveChanges();
            }
       
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ResetPassWord()
        {
            return View();
        }
        public JsonResult ResetPassWord(string Email)
        {
            var data = db.Users.Where(x => x.Email == Email).FirstOrDefault();
            string msg;
            if (data != null)
            {
                BuildForgotEmailTemplate(data.IDUser);
                msg = "True";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            msg = "False";          
            return Json(msg,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ResetPassWordComfirm(string IDUser)
        {
            ResetPassWord res = new ResetPassWord();
            res.IDUser = IDUser;
            return View(res);
        }
        public void BuildEmailTemplate(string UserName)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/UserLogin/") + "EmailTemplate" + ".cshtml");
            var regInfo = db.Users.Where(x => x.UserName == UserName).FirstOrDefault();
            var url = "https://localhost:44366/" + "UserLogin/Confirm?IdUser=" + regInfo.IDUser;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Tài khoản Rao Vặt Đã Được Đăng Ký", body, regInfo.Email);
        }
        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "trilxag1003@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from,"Rao Vặt");
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }
        public static void SendEmail(MailMessage mail)  
        {
           
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("trilxag1003@gmail.com", "miss_you6534");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index","Home");
        }
        //ForgotPassWord
        public void BuildForgotEmailTemplate(string IDUser)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/UserLogin/") + "ForgotEmailTemplate" + ".cshtml");
            var regInfo = db.Users.Where(x => x.IDUser == IDUser).FirstOrDefault();
            var url = "https://localhost:44366/" + "UserLogin/ResetPassWordComfirm?IdUser=" + regInfo.IDUser;
            body = body.Replace("@ViewBag.ResetPassWordLink", url);
            body = body.ToString();
            BuildEmailTemplate("Rao Vặt Khôi Phục Mật Khẩu", body, regInfo.Email);
        }
        [HttpPost]
        public ActionResult ResetPassWordComfirm(ResetPassWord model)
        {
            var Data = db.Users.Where(x => x.IDUser == model.IDUser).FirstOrDefault();
            Data.PassWord = new AccountDAO().GetMD5(model.PassWord);
            db.SaveChanges();
            ViewBag.success = "True";
            return View();
        }
    }
}