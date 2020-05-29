using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class Export_ClassPlacings
    {
        public IList<ClassPlacings_DT> Data { get; set; }
        public PermissionSettings UserPermissions { get; set; }

        public Export_ClassPlacings()
        {
            Data = new List<ClassPlacings_DT>();
        }
    }


    public class ClassPlacings_DT
    {
        public string RiderName { get; set; }

        public string HorseName { get; set; }

        public int ClassNumber { get; set; }

        public string ClassName { get; set; }

        public int Place { get; set; }

        public int? RiderNumber { get; set; }
    }
}