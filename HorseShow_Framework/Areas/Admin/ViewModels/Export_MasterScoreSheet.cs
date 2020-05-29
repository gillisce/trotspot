using HorseShow_Framework.Areas.Admin.ViewModels.Results;
using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class Export_MasterScoreSheet
    {
        public IList<MasterScores> Data { get; set; }
        public PermissionSettings UserPermissions { get; set; }

        public Export_MasterScoreSheet()
        {
            Data = new List<MasterScores>();
        }
    }


    public class MasterScores : RiderResults
    {
        public string DivisionName { get; set; }
    }
}