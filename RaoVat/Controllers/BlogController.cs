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
    public class BlogController : Controller
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
            var blogs = db.Blog.ToList();
            return View(blogs);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Detail(string slug)
        {
            var blogs = db.Blog.Where(x => x.slug == slug).FirstOrDefault();
            return View(blogs);
        }
    }
}