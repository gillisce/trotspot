using HorseShow_Framework.Models;
using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.KCrusade.ViewModels
{
    public class RegistrationMain_VM
    {
        public Person person { get; set; }

        public HorseRider horse { get; set; }

        public IList<Division> divisions { get; set; }

        public IList<StoreItems> storeItems { get; set; }

        public IList<int> lstExistingRiderNumbers { get; set; }

        public string strDivName { get; set; }

        public string strSelectedClasses { get; set; }
        public string strSelectedStoreItems { get; set; }

        public int totalDue { get; set; }

        public PermissionSettings UserPermissions { get; set; }


        public RegistrationMain_VM()
        {
            person = new Person();
            horse = new HorseRider();
            divisions = new List<Division>();
            storeItems = new List<StoreItems>();
            lstExistingRiderNumbers = new List<int>();
        }
    }
}