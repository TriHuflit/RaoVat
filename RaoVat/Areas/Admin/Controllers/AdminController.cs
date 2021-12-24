using RaoVat.DAO;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace RaoVat.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public RaoVatModel db = new RaoVatModel();
        // GET: Admin/Admin
        [Authorize (Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetListUser(string searchUser, int page = 1, int pageSize = 6)
        {
            IEnumerable<Users> listuser = db.Users.OrderBy(x=>x.UserName).ToPagedList(page,pageSize);
            if (!string.IsNullOrEmpty(searchUser))
            {
               listuser= db.Users.Where(x => x.UserName.Contains(searchUser)).OrderBy(x => x.DateCreate).ToPagedList(page,pageSize);
            }
            return View(listuser);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateUser()
        {        
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(string IDUser)
        {
          
            return View(db.Users.Where(s=>s.IDUser==IDUser).FirstOrDefault());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateUser(Users model)
        {
            if (ModelState.IsValid)
            {
                if (!new AccountDAO().CheckAccount(model.UserName))
                {
                    if(!new AccountDAO().CheckEmail(model.Email))
                    {
                        try
                        {
                            model.IDUser = db.Database.SqlQuery<string>("select dbo.fn_getRandom_Value()").SingleOrDefault();
                            model.DateCreate = DateTime.Now;
                            model.PassWord = new AccountDAO().GetMD5(model.PassWord);
                            model.EmailComfirm = true;
                            db.Users.Add(model);
                            db.SaveChanges();
                            return RedirectToAction("GetListUser");
                        }
                        catch
                        {
                            return Content("Error Create New");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Email already exists";
                    }
                }
                else
                {
                    ViewBag.Message = "Account already exists";                  
                }
                ModelState.Clear();
            }      
            return View();
          
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(string IDUser,Users users)
        {
            try
            {
                users.PassWord = new AccountDAO().GetMD5(users.PassWord);
                db.Entry(users).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); 
                return RedirectToAction("GetListUser");
            }
            catch
            {

                return View();
            }
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string IDUser,Users users)
        {
            try
            {
                users = db.Users.Where(s => s.IDUser == IDUser).FirstOrDefault();
                db.Users.Remove(users);
                db.SaveChanges();
                return RedirectToAction("GetListUser");

            }
            catch
            {
                return Content("This data is using in other table,Error Delete !");
            }
        }
    }
}