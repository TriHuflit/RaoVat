using Firebase.Auth;
using Firebase.Storage;
using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RaoVat.Context
{
    public class ConnectFireBase
    {
        public static RaoVatModel db = new RaoVatModel();
        public static string ApiKey = "AIzaSyBvVPyxAH6HUi6G4ywVitP6OQtslH6tro8";
        public static  string Bucket = "testraovat.appspot.com";
        public static string AuthEmail = "abc@gmail.com";
        public static string AuthPassword = "123abc";


        public static async  Task Upload(FileStream stream, string fileName, News news)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            string link = "";
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(Bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            }).Child("images").Child(news.IDNews+fileName ).PutAsync(stream, cancellation.Token);
            ImgNews imgNews = new ImgNews();
            imgNews.IDImg = db.Database.SqlQuery<string>("select dbo.fn_getRandom_ValueImg()").FirstOrDefault();
            imgNews.IDNews = news.IDNews;
            imgNews.ImgURL = await task;
            imgNews.NameImage = news.IDNews + fileName;
            db.ImgNews.Add(imgNews);
            db.SaveChanges();
            
            try
            {
                link = await task;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            
        }
       
        public static async void Delete(string name)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var firebaseStorage = new FirebaseStorage(Bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            }).Child("images").Child(name).DeleteAsync();
        }

    }
}