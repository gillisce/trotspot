using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels.Results
{
    public class BestInShow
    {
        public IList<RiderResults> results { get; set; }

        public PermissionSettings UserPermissions { get; set; }


        public BestInShow()
        {
            results = new List<RiderResults>();
        }
    }
}