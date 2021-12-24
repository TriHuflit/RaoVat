using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RaoVat.Areas.Admin.Models
{
    public class BrandModel
    {
        [Key]
        [StringLength(20)]
        public string IDBrand { get; set; }

        [StringLength(20)]
        public string IDSubCategory { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public string ImageBrand { get; set; }
        public List<SubCategory> ListSubcategory { get; set; }


    }
}