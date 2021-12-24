using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RaoVat.Models
{
    public class AccountLogin
    {
        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Tài khoản phải từ 8 ký tự trở lên")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật Khẩu không được bỏ trống")]
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
        public string PassWord { get; set; }
    }
}