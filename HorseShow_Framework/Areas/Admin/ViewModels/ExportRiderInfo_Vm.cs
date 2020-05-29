using HorseShow_Framework.Models;
using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class ExportRiderInfo_Vm
    {
        public IList<Export_DT> Data { get; set; }

        public PermissionSettings UserPermissions { get; set; }

        public ExportRiderInfo_Vm()
        {
            Data = new List<Export_DT>();
        }
    }

    public class Export_DT
    {
        public Person Person { get; set; }

        public HorseRider HorseRider { get; set; }

        public int? ClassCount { get; set; }

        public string strDivisionName { get; set; }
        public Export_DT()
        {
            Person = new Person();
            HorseRider = new HorseRider();
        }
    }
}