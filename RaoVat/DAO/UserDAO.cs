using RaoVat.Common;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaoVat.DAO
{
    public class UserDAO
    {
        private RaoVatModel context = null;

        public UserDAO()
        {
            context = new RaoVatModel();
        }
        public string ConvertImage(byte[] imageBrand)
        {
            string base64string = Convert.ToBase64String(imageBrand);
            return "data:image/jpg;base64," + base64string;
        }

        public Users ChangeInfo(Users model,Users user)
        {

            model.PassWord = new AccountDAO().GetMD5(model.PassWord);
            if (model.Balance == null)
                model.Balance = 0;
            user.FullName = model.FullName;
            if (model.Avatar != null)
            {
                user.Avatar = model.Avatar;
            }      
            user.PassWord = model.PassWord;
            user.Phone = model.Phone;
            user.Gender = model.Gender;
            user.Address = model.Address;
            user.Balance = model.Balance;
            user.IdentityCard = model.IdentityCard;
            user.Birth = model.Birth;
            user.Email = model.Email;
            if (user.Avatar != null)
            {
                user.Image = ConvertImage(user.Avatar);
            }
            else user.Image = "/Asset/img/user.png";
            return user;
        }
    }
}