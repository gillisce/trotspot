using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels.Analytics
{
    public class RiderStats
    {
        public IList<GeneralInfo> generalInfo { get; set; }

        public ShowStats ShowStats { get; set; }

        public PermissionSettings UserPermissions { get; set; }

        public RiderStats()
        {
            generalInfo = new List<GeneralInfo>();
            ShowStats = new ShowStats();
        }
    }


    public class GeneralInfo
    {
        public int PersonID { get; set; }

        public int Age { get; set; }
        public int ClassCount { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PaymentMethod { get; set; }

        public int? ZipCode { get; set; }
        public string DivisionName { get; set; }
    }


    public class ShowStats
    {
        public IList<ValuePairs> numRidersByDivision { get; set; }
        public IList<ValuePairs> numRidersDuplicatName { get; set; }
        public IList<ValuePairs> amountSpentByRider { get; set; }
        public IList<ValuePairs> classCountByRider { get; set; }

        public IList<ValuePairs> ridersInClass { get; set; }

        public ShowStats()
        {
            numRidersByDivision = new List<ValuePairs>();
            numRidersDuplicatName = new List<ValuePairs>();
            amountSpentByRider = new List<ValuePairs>();
            classCountByRider = new List<ValuePairs>();
            ridersInClass = new List<ValuePairs>();
        }
    }

    public class ValuePairs
    {
        public int count { get; set; }
        public string Description { get; set; }
    }
}