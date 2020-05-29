using HorseShow_Framework.Areas.Privacy.ViewModels;
using HorseShow_Framework.HelperClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorseShow_Framework.Areas.Privacy.Controllers
{
    public class PrivacyController : Controller
    {
        readonly SetUpPermissions setUpPermissions = new SetUpPermissions();

        // GET: Privacy/Privacy
        public ActionResult Index()
        {
            Home_VM @page = new Home_VM()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(User.Identity.GetUserName())
            };
            return View("PrivacyContent",@page);
        }
    }
}