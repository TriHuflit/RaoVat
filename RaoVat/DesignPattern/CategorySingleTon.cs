using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaoVat.DesignPattern
{
    public sealed class CategorySingleTon
    {
        public static CategorySingleTon Instance { get; } = new CategorySingleTon();
        public List<Category> listCategory { get; } = new List<Category>();
        private CategorySingleTon() { }

        public void Init(RaoVatModel db)
        {
            if (listCategory.Count == 0)
            {
                var categories = db.Category.ToList();
                foreach (var item in categories)
                {
                    listCategory.Add(item);
                }
            }

        }
        public void Update(RaoVatModel db)
        {
            listCategory.Clear();
            Init(db);
        }


    }
}