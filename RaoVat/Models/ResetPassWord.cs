using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RaoVat.Models
{
    public class ResetPassWord
    {
        public string IDUser { get; set; }
        [Required(ErrorMessage = "Mật Khẩu không được bỏ trống")]
        [StringLength(maximumLength:16, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Compare("PassWord", ErrorMessage = "Mật khẩu không giống nhau")]
        public string ComfirmPassWord { get; set; }
    }
}