namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Globalization;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            BoxSave = new HashSet<BoxSave>();
            ImgNews = new HashSet<ImgNews>();
            Reports = new HashSet<Reports>();
        }

        [Key]
        [StringLength(20)]
        public string IDNews { get; set; }

        [StringLength(20)]
        public string IDUser { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [StringLength(20)]
        public string IDBrand { get; set; }

        [Required(ErrorMessage ="Tên bài đăng không được bỏ trống")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giá Tiền không được bỏ trống")]
        public double? Price { get; set; }
        public DateTime? Time { get; set; }
        [Required(ErrorMessage = "Mô tả không được bỏ trống")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [StringLength(100)]
        public string Address { get; set; }
        public bool? Type { get; set; }
        public byte? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoxSave> BoxSave { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImgNews> ImgNews { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports { get; set; }

      
        [NotMapped]
        public List<Category> listCate { get; set; }
        [NotMapped]
        public string nameBrand { get; set; }
        [NotMapped]
        public string Image { get; set; }

        [NotMapped]
        public List<string> listImage { get; set; }
    }
}
