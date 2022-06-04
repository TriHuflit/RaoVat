using System.Web.Mvc;

namespace RaoVat.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
               defaults: new {controller="Admin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
               name: "Admin_Blog",
               url: "Admin/{controller}/{action}/{slug}",
               defaults: new { controller = "AdminBlog", action = "Update" }
            );
        }
    }
}