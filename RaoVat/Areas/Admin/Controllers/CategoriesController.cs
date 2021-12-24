using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;
using RaoVat.DesignPattern;

namespace RaoVat.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        private readonly RaoVatModel db;

        public CategoriesController()
        {
            db = new RaoVatModel();
            CategorySingleTon.Instance.Init(db);
        }
        //Method
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        public byte[] ReadImage(HttpPostedFileBase Image)
        {
            byte[] img = new byte[Image.ContentLength];
            Image.InputStream.Read(img, 0, Image.ContentLength);
            return img;
        }

        //HTTPGet
        //Category
        [Authorize(Roles ="Admin")]
        [HttpGet]
       
        public ActionResult Index(string searchCategories, int page=1,int pageSize=6)
        {
            IEnumerable<Category> listcategory = db.Category.OrderBy(x => x.IDCategory).ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(searchCategories))
            {
                listcategory = db.Category.Where(x => x.Name.Contains(searchCategories)).OrderBy(x => x.IDCategory).ToPagedList(page, pageSize);
            }
            foreach (var item in listcategory)
            {
                if (item.ImageCate != null)
                {
                    item.Image = ConvertImage(item.ImageCate);
                }
            }
            return View(listcategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]    
        public ActionResult CreateCategory()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]   
        public ActionResult EditCategory(string IDCategory)
        {
            Category category = db.Category.Where(x => x.IDCategory == IDCategory).FirstOrDefault();
            if (category.ImageCate!=null)
            {
                category.Image = ConvertImage(category.ImageCate);
                TempData["imgData"] = category.ImageCate;
            }      
            return View(category);
        }
        //SubCategory
        [Authorize(Roles = "Admin")]
        [HttpGet]      
        public ActionResult SubCategory(string searchSubCate,int page=1,int pageSize=6)
        {
            IEnumerable<SubCategory> listsubcate = db.SubCategory.OrderBy(x => x.IDSubCategory).ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(searchSubCate))
            {
                listsubcate = db.SubCategory.Where(x => x.Name.Contains(searchSubCate) || x.Category.Name.Contains(searchSubCate)).OrderBy(x => x.IDSubCategory).ToPagedList(page, pageSize);
            }
            foreach (var item in listsubcate)
            {
                if (item.ImageSubCate != null)
                {
                    item.Image = ConvertImage(item.ImageSubCate);
                }
            }
            return View(listsubcate);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]      
        public ActionResult CreateSubCate()
        {
            SubCategory subCategory = new SubCategory();
            subCategory.ListCategory = db.Category.ToList();
            TempData["ListCategory"] = subCategory.ListCategory;
            return View(subCategory);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditSubCate(string IDSubCategory)
        {
            SubCategory subCategory = db.SubCategory.Where(x => x.IDSubCategory == IDSubCategory).FirstOrDefault();
            subCategory.ListCategory = db.Category.ToList();
            TempData["ListCategory"] = subCategory.ListCategory;
            if (subCategory.ImageSubCate != null)
            {
                subCategory.Image = ConvertImage(subCategory.ImageSubCate);
                TempData["imgData"] = subCategory.ImageSubCate;
            }
            return View(subCategory);
        }
        //Brand
        [Authorize(Roles = "Admin")]
        [HttpGet]
      
        public ActionResult Brand(string searchBrand, int page = 1, int pageSize = 6)
        {
            IEnumerable<Brand> listbrand = db.Brand.OrderBy(x => x.IDBrand).ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(searchBrand))
            {
                listbrand = db.Brand.Where(x => x.Name.Contains(searchBrand) || x.SubCategory.Name.Contains(searchBrand)).OrderBy(x => x.IDBrand).ToPagedList(page, pageSize);
            }
            foreach (var item in listbrand)
            {
                if (item.ImageBrand != null)
                {
                    item.Image = ConvertImage(item.ImageBrand);
                }
            }
            return View(listbrand);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]  
        public ActionResult CreateBrand()
        {
            Brand brand = new Brand();
            brand.ListSubCategories = db.SubCategory.ToList();
            TempData["ListSubCategory"] =brand.ListSubCategories;
            return View(brand);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]     
        public ActionResult EditBrand(string IDBrand)
        {
           
            Brand brand = db.Brand.Where(x => x.IDBrand == IDBrand).FirstOrDefault();
            brand.ListSubCategories = db.SubCategory.ToList();
            TempData["ListSubcategory"] = brand.ListSubCategories;
            if (brand.ImageBrand != null)
            {
                brand.Image = ConvertImage(brand.ImageBrand);
                TempData["imgData"] = brand.ImageBrand;
            }
            return View(brand);
        }

        //HTTPPost
        //Category
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(HttpPostedFileBase Image,Category model)
        {
            if (Image != null)
            {
                model.ImageCate =ReadImage(Image);
               
                ModelState["ImageCate"].Errors.Clear();
                ModelState["ImageCate"].Value = new ValueProviderResult(model.ImageCate, "", new CultureInfo("en-GB"));
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    db.Category.Add(model);
                    db.SaveChanges();
                    CategorySingleTon.Instance.Update(db);
                    return RedirectToAction("Index", "Categories");
                }
                catch
                {
                    return Content("Danh mục đã tồn tại");
                }
            }
            else
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(HttpPostedFileBase Image,Category model)
        {
            if (Image != null)
            {
                model.ImageCate=ReadImage(Image);          
            }
            else
            {
                model.ImageCate = (byte[])TempData["imgData"];
            }
            ModelState["ImageCate"].Errors.Clear();
            ModelState["ImageCate"].Value = new ValueProviderResult(model.ImageCate, "", new CultureInfo("en-GB"));
            if (ModelState.IsValid)
            { 
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                CategorySingleTon.Instance.Update(db);
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCategory(string IDCategory)
        {
            try
            {
                Category cate = db.Category.Where(x => x.IDCategory == IDCategory).FirstOrDefault();
                db.Category.Remove(cate);
                db.SaveChanges();
                CategorySingleTon.Instance.Update(db);
                return RedirectToAction("Index", "Categories");
            }
            catch
            {
                return Content("Đối tượng xóa có dính ràng buộc !");
            }

        }
        //SubCategory
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateSubCate(HttpPostedFileBase Image,SubCategory model)
        {
            if (Image != null)
            {
                model.ImageSubCate =ReadImage(Image);            
                ModelState["ImageSubCate"].Errors.Clear();
                ModelState["ImageSubCate"].Value = new ValueProviderResult(model.ImageSubCate, "", new CultureInfo("en-GB"));
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.SubCategory.Add(model);
                    db.SaveChanges();
                    CategorySingleTon.Instance.Update(db);
                    return RedirectToAction("SubCategory", "Categories");
                }
                catch
                {
                    return Content("Danh Mục Con đã tồn tại");
                }
            }
            else
            {
                model.ListCategory = (List<Category>)TempData["ListCategory"];
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteSubCate(string IDSubCategory)
        {
            try
            {
                SubCategory subcate = db.SubCategory.Where(x => x.IDSubCategory == IDSubCategory).FirstOrDefault();
                db.SubCategory.Remove(subcate);
                db.SaveChanges();
                CategorySingleTon.Instance.Update(db);
                return RedirectToAction("SubCategory", "Categories");
            }
            catch
            {
                return Content("Error constraint for Category already exists !");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCate(HttpPostedFileBase Image,SubCategory model)
        {
            if (Image != null)
            {
                model.ImageSubCate =ReadImage(Image);            
            }
            else
            {
                model.ImageSubCate = (byte[])TempData["imgData"];
            }
            ModelState["ImageSubCate"].Errors.Clear();
            ModelState["ImageSubCate"].Value = new ValueProviderResult(model.ImageSubCate, "", new CultureInfo("en-GB"));
            if (ModelState.IsValid)
            {

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                CategorySingleTon.Instance.Update(db);
                return RedirectToAction("SubCategory", "Categories");
            }
            else
            {
                model.ListCategory = (List<Category>)TempData["ListCategory"];
                return View(model);
            }
        }
        //Brand
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(HttpPostedFileBase Image, Brand model)
        {
            if (Image != null)
            {
                model.ImageBrand = ReadImage(Image);
                ModelState["ImageBrand"].Errors.Clear();
                ModelState["ImageBrand"].Value = new ValueProviderResult(model.ImageBrand, "", new CultureInfo("en-GB"));
            }
         
            if (ModelState.IsValid)
            {
                try
                {
                    db.Brand.Add(model);
                    db.SaveChanges();
                    CategorySingleTon.Instance.Update(db);
                    return RedirectToAction("Brand", "Categories");
                }
                catch
                {
                    return Content("Nhãn hiệu đã tồn tại");
                }
            }
            else
            {
                model.ListSubCategories = (List<SubCategory>)TempData["ListSubcategory"];
                return View(model);
            }
           
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBrand(string IDBrand)
        {
            try
            {
                Brand brand = db.Brand.Where(x => x.IDBrand == IDBrand).FirstOrDefault();
                db.Brand.Remove(brand);
                db.SaveChanges();
                CategorySingleTon.Instance.Update(db);
                return RedirectToAction("Brand", "Categories");
            }
            catch
            {
                return Content("Error constraint for News already exists ! !");
            }
          
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(HttpPostedFileBase Image,Brand brand)
        {
            if (Image != null)
            {
                brand.ImageBrand = ReadImage(Image);
            }
            else
            {              
                brand.ImageBrand = (byte[])TempData["imgData"];            
            }
            ModelState["ImageBrand"].Errors.Clear();
            ModelState["ImageBrand"].Value = new ValueProviderResult(brand.ImageBrand, "", new CultureInfo("en-GB"));
            if (ModelState.IsValid)
            {
                Brand b = db.Brand.Where(x => x.IDBrand == brand.IDBrand).FirstOrDefault();
                b.ImageBrand = brand.ImageBrand;
                b.Name = brand.Name;
                b.IDSubCategory = brand.IDSubCategory;
                db.SaveChanges();
                CategorySingleTon.Instance.Update(db);
                return RedirectToAction("Brand", "Categories");
            }
            else
            {
               brand.ListSubCategories = (List<SubCategory>)TempData["ListSubcategory"];
               return View(brand);
            }
           
        }
    }
}