using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class Export_ClassLineUps
    {
        public IList<ClassLines_Export> classLines_Registered { get; set; }
        public IList<ClassLines_Export> classLines_CheckedIn { get; set; }

        public PermissionSettings UserPermissions { get; set; }

        public Export_ClassLineUps()
        {
            UserPermissions = new PermissionSettings();
            classLines_Registered = new List<ClassLines_Export>();
            classLines_CheckedIn = new List<ClassLines_Export>();
        }
    }


    public class ClassLines_Export : ClassRiders
    {
        new public int? intRiderNumber { get; set; }
        public bool CheckedIn { get; set; }

        public string Division { get; set; }

    }
}