using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;

using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RaoVat.DAO
{
    public class AccountDAO
    {
        private RaoVatModel context = null;
     
        public AccountDAO()
        {
            context = new RaoVatModel();
        }


        public bool Login(string UserName,string PassWord)
        {
            PassWord= GetMD5(PassWord);
            object[] parameter =
            {
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@PassWord",PassWord)
            };
            var result = context.Database.SqlQuery<bool>("USP_Login @UserName,@PassWord",parameter).SingleOrDefault();
            return result;
        }
        public bool CheckAccount(string UserName)
        {
            object[] parameter =
            {
                new SqlParameter("@UserName",UserName),
              
            };
            var result = context.Database.SqlQuery<bool>("USP_CheckAccout @UserName", parameter).SingleOrDefault();
            return result;
        }
        public bool CheckEmail(string Email)
        {
            object[] parameter =
            {
                new SqlParameter("@Email",Email),

            };
            var result = context.Database.SqlQuery<bool>("USP_GmailAddress @Email", parameter).SingleOrDefault();
            return result;
        }
        public Users GetInfoByUserName(string UserName)
        {

            return context.Users.SingleOrDefault(x=>x.UserName== UserName);
        }
        public string CheckRegister(string UserName, string PassWord, string Email)
        {
            string res;
            switch (new AccountDAO().Register(UserName, PassWord, Email))
            {
                case -1:
                    res = "Tài khoản đã tồn tại";
                    break;
                case 0:
                    res = "Email đã tồn tại";
                    break;
                default:
                    res = "";
                    break;
            }
            return res;
        }
        public int Register(string UserName,string PassWord,string Email)
        {
           
            if (CheckAccount(UserName))
            { 
                return -1;
            }
            if (CheckEmail(Email))
            {
                return 0;
            }
            PassWord = GetMD5(PassWord);         
            object[] parameter =
            {           
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@PassWord",PassWord),
                new SqlParameter("@Email",Email)
            };
            
            var result = context.Database.ExecuteSqlCommand("USP_Register @UserName,@PassWord,@Email",parameter);  
            return 1;
        }
        string hash = "f0xle@rn";
        public string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromdata = UTF8Encoding.UTF8.GetBytes(str);
            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

            string data;
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] result = transform.TransformFinalBlock(fromdata, 0, fromdata.Length);
                data = Convert.ToBase64String(result, 0, result.Length);
            }
            return data;
        }
        public string ToHex(string str)
        {
            string data;
            byte[] fromdata =Convert.FromBase64String(str);
            using(MD5CryptoServiceProvider md5  = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] result = transform.TransformFinalBlock(fromdata, 0, fromdata.Length);
                    data = UTF8Encoding.UTF8.GetString(result);
                }
            } 
            return data;
        }
    }
}