using System.Web.Mvc;

namespace mvc5_first.Areas.SPA
{
    public class SPAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SPA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SPA_default",
                "SPA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "mvc5_first.Areas.SPA.Controllers" }
            );
        }
    }
}