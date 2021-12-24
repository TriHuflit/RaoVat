using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RaoVat.Areas.Admin.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật Khẩu không được bỏ trống")]
        public string PassWord { get; set; }
    }
}