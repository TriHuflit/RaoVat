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
using System.IO;
using System.Threading.Tasks;
using RaoVat.Context;

namespace RaoVat.Areas.Admin.Controllers
{
    public class AdminBlogController : Controller
    {
        public RaoVatModel db = new RaoVatModel();
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            var blogs = db.Blog.ToList();
            return View(blogs);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Detail(string slug)
        {
            var blogs = db.Blog.Where(x => x.slug == slug).FirstOrDefault();
            return View(blogs);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
       
        public async Task<ActionResult> Create(Blog blog,HttpPostedFileBase Image)
        {
            blog.DateCreate = DateTime.Now;
            blog.IDBlog = db.Database.SqlQuery<string>("select dbo.fn_getRandom_ValueImg()").FirstOrDefault();
            blog.ImgURL = "imgurl";
            blog.NameImage = "nameImg";
            FileStream steam;
            if (Image.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Content/images/"), Image.FileName);
                Image.SaveAs(path);
                steam = new FileStream(Path.Combine(path), FileMode.Open);
                await Task.Run(() => ConnectFireBase.UploadBlog(steam, Image.FileName, blog, db));
            }
           
            ViewBag.Message = "Thêm blog thành công";
            return RedirectToAction("Index", "AdminBlog");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Update(string slug)
        {
            var blog = db.Blog.Where(x=>x.slug == slug).FirstOrDefault();
            if(blog != null)
            {
                blog.Image = blog.ImgURL;
            }
            return View(blog);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Update(Blog newblog, HttpPostedFileBase Image)
        {

            var blog = db.Blog.Where(x => x.IDBlog == newblog.IDBlog).FirstOrDefault();
            blog.Author = newblog.Author;
            blog.Titile = newblog.Titile;
            blog.Content = newblog.Content;
            blog.Description = newblog.Description;
            blog.slug = newblog.slug;
            blog.Type = newblog.Type;        
            if (Image != null)
            {
                await Task.Run(() => ConnectFireBase.Delete(blog.NameImage));
                blog.ImgURL = "imgurl";
                blog.NameImage = "nameImg";
                FileStream steam;
                if (Image.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/images/"), Image.FileName);
                    Image.SaveAs(path);
                    steam = new FileStream(Path.Combine(path), FileMode.Open);
                    await Task.Run(() => ConnectFireBase.UpdateBlog(steam, Image.FileName, blog,db));
                }
            }
            else
            {
                db.Entry(blog).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "AdminBlog");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(string slug)
        {
            var blog = db.Blog.Where(x => x.slug == slug).FirstOrDefault();
            if (blog != null)
            {
                blog.Image = blog.ImgURL;
            }
            return View(blog);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(Blog blog)
        {
            blog = db.Blog.Where(x => x.slug == blog.slug).FirstOrDefault();
            await Task.Run(() => ConnectFireBase.Delete(blog.NameImage));
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index", "AdminBlog");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Duplicate(string slug)
        {
            var blog = db.Blog.Where(x => x.slug == slug).FirstOrDefault();
            if (blog != null)
            {
                blog.Image = blog.ImgURL;
            }
            return View(blog);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Duplicate(Blog blog)
        {
            blog = db.Blog.Where(x => x.slug == blog.slug).FirstOrDefault();
            string filename = blog.ImgURL;
            string IDBlog = db.Database.SqlQuery<string>("select dbo.fn_getRandom_ValueImg()").FirstOrDefault();
            Blog cloneBlog = blog.Clone(IDBlog);
            await Task.Run(() => ConnectFireBase.DuplicateBlog(filename, cloneBlog, db));
            return RedirectToAction("Index", "AdminBlog");
        }
    }
}