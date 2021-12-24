namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImgNews
    {
        [Key]
        [StringLength(10)]
        public string IDImg { get; set; }

        [StringLength(20)]
        public string IDNews { get; set; }

        public string ImgURL { get; set; }

        public string NameImage { get; set; }
        public virtual News News { get; set; }


        [NotMapped]
        public string Image { get; set; }
    }
}
