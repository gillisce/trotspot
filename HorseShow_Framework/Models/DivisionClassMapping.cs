using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class DivisionClassMapping
    {
        [Key]
        [Display(Name = "DivisionClass Combo")]
        public int intDivClassID { get; set; }

        [Display(Name = "Division ID")]
        public int intDivisionID { get; set; }

        [Display(Name = "Division Name")]
        public string strDivisionName { get; set; }

        [Display(Name = "Class Number")]
        public int intClassID { get; set; }

        [Display(Name = "Class Name")]
        public string strClassName { get; set; }

        [Display(Name = "Is Active?")]
        public bool ysnActive { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }

        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }
    }
}