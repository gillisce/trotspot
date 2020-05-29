using System.Web.Mvc;

namespace HorseShow_Framework.Areas.KCrusade
{
    public class KCrusadeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KCrusade";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KCrusade_default",
                "KCrusade/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}