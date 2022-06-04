using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaoVat.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListFriend()
        {
            return PartialView();
        }

        public ActionResult ChatBox()
        {
            return PartialView();
        }
    }
}