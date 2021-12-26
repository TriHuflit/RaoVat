using RaoVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaoVat.DesignPattern
{
    public interface IIterator
    {
        News First();
        News Next();
        bool isDone { get; }
        News CurrentItem { get; }   
    }
    public class NewsIterator : IIterator
    {
        List<News> listNews;
        int current = 0;
        int step = 1;
        public NewsIterator(List<News> listNews)
        {
            this.listNews = listNews;
        }
        public bool isDone { get { return current >= listNews.Count; } }

        public News CurrentItem => listNews[current];

        public News First()
        {
            if (listNews.Count > 0)
            {
                current = 0;
                return listNews[current];
            }
            return null;
        }

        public News Next()
        {
            current +=step;
            if(isDone)
                return null;
            else return listNews[current];
        }
    }
}
