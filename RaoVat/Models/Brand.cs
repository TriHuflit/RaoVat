namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brand()
        {
            News = new HashSet<News>();
        }

        [Key]
        [StringLength(20)]
        [Required(ErrorMessage = "Mã không được bỏ trống")]
        public string IDBrand { get; set; }

        [StringLength(20)]
     
        public string IDSubCategory { get; set; }
        [Required(ErrorMessage ="Tên không được bỏ trống")]
        [StringLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hình ảnh không được bỏ trống")]
        public byte[] ImageBrand { get; set; }
        
        public virtual SubCategory SubCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }

        [NotMapped]
        public string Image { get; set; }
        [NotMapped]
        public List<SubCategory> ListSubCategories { get; set; }
    }
}
