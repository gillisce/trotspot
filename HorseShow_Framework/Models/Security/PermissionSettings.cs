using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models.Security
{
    public class PermissionSettings
    {
        public bool Showbill_Settings { get; set; }
        public bool CheckIn_Riders { get; set; }
        public bool View_ClassLineUp { get; set; }
        public bool PlaceClass { get; set; }
        public bool View_DivScores { get; set; }
        public bool View_ClassScores { get; set; }
        public bool BestInShow_Results { get; set; }
        public bool Admin { get; set; }
        public bool Developer { get; set; }
    }
}