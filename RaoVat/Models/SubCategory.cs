namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubCategory")]
    public partial class SubCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubCategory()
        {
            Brand = new HashSet<Brand>();
        }

        [Key]
        [Required(ErrorMessage = "Mã danh mục con không được bỏ trống")]
        [StringLength(20)]
        public string IDSubCategory { get; set; }

        
        [StringLength(20)]
        public string IDCategory { get; set; }

        [Required(ErrorMessage = "Tên danh mục con không được bỏ trống")]
        [StringLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Hình Ảnh không được bỏ trống")]
        public byte[] ImageSubCate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Brand> Brand { get; set; }

        public virtual Category Category { get; set; }


        [NotMapped]
        public string Image { get; set; }
        [NotMapped]
        public List<Category> ListCategory { get; set; }
    }
}
