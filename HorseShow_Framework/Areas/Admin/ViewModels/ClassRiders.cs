using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class ClassRiders
    {
        public string strClassName { get; set; }
        public int? intClassNum { get; set; }

        public string strHorseName { get; set; }

        public string strFirstName { get; set; }

        public string strLastName { get; set; }

        public int intRiderNumber { get; set; }

        public int intHorseRiderID { get; set; }

        public IList<ClassRiders> classRiders { get; set; }

        public ClassRiders()
        {
            classRiders = new List<ClassRiders>();
        }
    }
}