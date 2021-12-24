using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RaoVat.Models
{
    public class Register
    {
        [Key]
        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Tài khoản phải từ 8 ký tự trở lên")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật Khẩu không được bỏ trống")]
        [StringLength(maximumLength:16,MinimumLength =8,ErrorMessage ="Mật khẩu phải từ 8 ký tự trở lên")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Compare("PassWord",ErrorMessage ="Mật khẩu không giống nhau")]
        public string ComfirmPassWord { get; set; }  
     
        [Required(ErrorMessage ="Email không được bỏ trống")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Email chưa đúng")]
        public string Email { get; set; }
    }
}