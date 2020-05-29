using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class Division_VM
    {
        [Key]
        [Display(Name = "Division ID")]
        public int intDivisionID { get; set; }

        [Display(Name = "Division Name")]
        public string strDivisionName { get; set; }

        [Display(Name = "Division Description")]
        public string strDivisionDescr { get; set; }

        [Display(Name = "Max Age")]
        public int? intMaxAge { get; set; }

        [Display(Name = "Min Age")]
        public int? intMinAge { get; set; }

        [Display(Name = "Max Num Riders")]
        public int? intMaxNumRiders { get; set; }

        [Display(Name = "Min Num Riders")]
        public int? intMinNumRiders { get; set; }

        [Display(Name = "Is Active")]
        public bool ysnActive { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }

        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }
    
        public IList<Division> divisions { get; set; }

        public Division_VM()
        {
            divisions = new List<Division>();
        }
    }
}
