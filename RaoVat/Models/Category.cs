namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        [Key]
        [StringLength(20)]
        [Required(ErrorMessage = "Mã danh mục không được bỏ trống")]
        public string IDCategory { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Tên danh mục không được bỏ trống")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Hình ảnh không được bỏ trống")]
        public byte[] ImageCate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubCategory> SubCategory { get; set; }

        [NotMapped]
        public string Image { get; set; }
        
    }
}
