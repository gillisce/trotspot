using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class RegisteredRider_VM
    {
        public int intHorseRiderID { get; set; }
        public string strRiderName { get; set; }

        public string strHorseName { get; set; }

        public string strDivisionRegistered { get; set; }

        public int? intRiderNumber { get; set; }

        public int intTotalDue { get; set; }

        public bool isCheckedIn { get; set; }

        public bool isPaid { get; set; }

        public string strPaymentMethod { get; set; }

        public int? intClassCount { get; set; }

       public IList<RegisteredRider_VM> listRiders { get; set; }
        public PermissionSettings UserPermissions { get; set; }

        public RegisteredRider_VM()
        {
            listRiders = new List<RegisteredRider_VM>();
        }
    }
}