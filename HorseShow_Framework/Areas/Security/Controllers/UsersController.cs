using HorseShow_Framework.Areas.Security.Models;
using HorseShow_Framework.HelperClasses;
using HorseShow_Framework.Models.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorseShow_Framework.Areas.Security.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        readonly SetUpPermissions setUpPermissions = new SetUpPermissions();
        // GET: Users
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public ActionResult Index()
        {
            Permissions_VM model = new Permissions_VM()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(User.Identity.GetUserName())
            };
            return RedirectToAction("Index", "Home", new { Area = "KCrusade" });
        }
    }
}