using HorseShow_Framework.Models;
using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels.Results
{
    public class DivisionBreakDown
    {
        public List<Division> divs { get; set; }
       public IList<RiderResults> results { get; set; }

        public PermissionSettings UserPermissions { get; set; }

        public DivisionBreakDown()
        {
            results = new List<RiderResults>();
        }
    }



    public class RiderResults
    {
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        public string strFullName { get; set; }

        public string strHorseName { get; set; }
        public int intRiderNum { get; set; }
        public int lvlID { get; set; }
        public string strName { get; set; }

        public int intTotalScore { get; set; }
        public int int1stPlaceCount { get; set; }
        public int int2PlaceCount { get; set; }
        public int int3PlaceCount { get; set; }
        public int int4PlaceCount { get; set; }
        public int int5PlaceCount { get; set; }
    }
}