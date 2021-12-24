namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;

    interface IInfoUser
    {
        string UpdateProfile(RaoVatModel db);
    }
    public class ProxyUser : IInfoUser
    {
        private Users users;
        public ProxyUser(Users users)
        {
            this.users = users;
        }
        public string UpdateProfile(RaoVatModel db)
        {
            
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/TrackData/Check_FullName.txt");
            foreach(string item in File.ReadLines(path))
            {
                if (users.FullName.ToLower().Contains(item.ToLower()))
                {
                    return "Ivalid FullName";
                }
            }
            users.UpdateProfile(db);
            return "Valid";
        }
    }
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            BoxSave = new HashSet<BoxSave>();
            HistoryService = new HashSet<HistoryService>();
            News = new HashSet<News>();
            RoomChat = new HashSet<RoomChat>();
            RoomChat1 = new HashSet<RoomChat>();
        }

        [Key]
        [StringLength(20)]
        public string IDUser { get; set; }

        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [StringLength(50)]
        public string PassWord { get; set; }

      
        [StringLength(8)]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreate { get; set; }

        [Required(ErrorMessage = "Họ tên không được bỏ trống")]
        [StringLength(30)]
        public string FullName { get; set; }
        [StringLength(5)]
        public string Gender { get; set; }

        //[Column(TypeName = "date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }

        public byte[] Avatar { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại nhập sai !!!")]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [StringLength(12)]
        public string IdentityCard { get; set; }

        public int? Balance { get; set; }

        public bool EmailComfirm { get; set; }

        public enum ChucVu
        {
            User,
            VipUser,
            Admin
        }
        public enum GioiTinh
        {
            Nam,
            Nữ
        }
        
        public void UpdateProfile(RaoVatModel db)
        {
            db.Entry(this).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoxSave> BoxSave { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoryService> HistoryService { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomChat> RoomChat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomChat> RoomChat1 { get; set; }


        [NotMapped]
        public string Image { get; set; }
    }
}
