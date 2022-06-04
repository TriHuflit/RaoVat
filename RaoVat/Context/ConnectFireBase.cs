using Firebase.Auth;
using Firebase.Storage;
using RaoVat.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RaoVat.Context
{
    public class ConnectFireBase
    {
        public static string ApiKey = "yourkey";
        public static  string Bucket = "yourBucket";
        public static string AuthEmail = "youremail";
        public static string AuthPassword = "yourpassword";


        public static async  Task Upload(FileStream stream, string fileName, News news,RaoVatModel db)
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
        public static async Task UploadBlog(FileStream stream, string fileName, Blog blog, RaoVatModel db)
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
            }).Child("images").Child(blog.IDBlog + fileName).PutAsync(stream, cancellation.Token);
            blog.ImgURL = await task;
            blog.NameImage = blog.IDBlog + fileName;
            db.Blog.Add(blog);
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
        public static async Task UpdateBlog(FileStream stream, string fileName, Blog blog,RaoVatModel db)
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
            }).Child("images").Child(blog.IDBlog + fileName).PutAsync(stream, cancellation.Token);
            blog.ImgURL = await task;
            blog.NameImage = blog.IDBlog + fileName;
            db.Entry(blog).State = System.Data.Entity.EntityState.Modified;
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
        private static Stream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);

            return new MemoryStream(imageData);
        }
        public static async Task DuplicateBlog(string filename,Blog blog, RaoVatModel db)
        {
            Stream stream= GetStreamFromUrl(filename);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            string link = "";
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(Bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            }).Child("images").Child(blog.IDBlog + blog.slug).PutAsync(stream, cancellation.Token);
            blog.ImgURL = await task;
            blog.NameImage = blog.IDBlog + filename;
            db.Blog.Add(blog);
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