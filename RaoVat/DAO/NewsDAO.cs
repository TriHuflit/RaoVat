using RaoVat.Context;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RaoVat.DAO
{
    public class NewsDAO
    {
        private RaoVatModel context = null;

        public NewsDAO()
        {
            context = new RaoVatModel();
        }
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }
        public void EditNews(News model)
        {
  
            var editnews = context.News.Where(x => x.IDNews == model.IDNews).FirstOrDefault();
            editnews.Name = model.Name;
            editnews.Price = model.Price;
            editnews.Description = model.Description;
            editnews.Address = model.Address;
            editnews.Type = model.Type;
            editnews.Status = 0;
            var brand = context.Brand.Where(x => x.IDBrand == model.IDBrand).FirstOrDefault();
            model.nameBrand = brand.SubCategory.Name + " " + brand.Name;
            context.Entry(editnews).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        
        public void EditnewsRefuse(HttpPostedFileBase[] Images, News model)
        {
            var listImg = context.ImgNews.Where(x => x.IDNews == model.IDNews).ToList();
            context.ImgNews.RemoveRange(listImg);
            context.SaveChanges();
         
            var editnews = context.News.Where(x => x.IDNews == model.IDNews).FirstOrDefault();
            editnews.Name = model.Name;
            editnews.Price = model.Price;
            editnews.Description = model.Description;
            editnews.Address = model.Address;
            editnews.Type = model.Type;
            editnews.Status = 0;
            var brand = context.Brand.Where(x => x.IDBrand == model.IDBrand).FirstOrDefault();
            model.nameBrand = brand.SubCategory.Name + " " + brand.Name;
            context.Entry(editnews).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
  
        
    }
}