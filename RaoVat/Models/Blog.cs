namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Blog")]
    public partial class Blog:BlogPrototype
    {
        [Key]
        [StringLength(10)]
        public string IDBlog { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public DateTime? DateCreate { get; set; }
        [StringLength(100)]
        public string Type { get; set; }
        [StringLength(100)]
        public string Titile { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        [StringLength(150)]
        public string slug { get; set; }
        public string ImgURL { get; set; }

        public string NameImage { get; set; }

        [NotMapped]
        public string Image { get; set; }

        public Blog Clone(string IDBlog)
        {
            Blog blog= new Blog();
            blog.IDBlog=IDBlog;
            blog.Author = Author;
            blog.DateCreate = DateTime.Now;
            blog.Type = Type;
            blog.Titile = Titile;
            blog.Description = Description;
            blog.Description=Description;
            blog.Content = Content;
            blog.slug = slug + "Copy";
            return blog;
        }
    }
    public interface BlogPrototype
    {
        Blog Clone(string IDBlog);
    }
}
