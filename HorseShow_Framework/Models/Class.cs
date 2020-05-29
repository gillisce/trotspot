using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class Class
    {
        [Key]
        [Display(Name = "Class ID")]
        public int intClassID { get; set; }

        [Display(Name ="Class Number")]
        public int intClassNumber { get; set; }

        [Display(Name = "Class Name")]
        public string strClassName { get; set; }

        [Display(Name ="Class Cost")]
        [DataType(DataType.Currency)]
        public float? fltCost { get; set; }

        [Display(Name = "Max Num Riders")]
        public int? intMaxNumRiders { get; set; }

        [Display(Name = "Min Num Riders")]
        public int? intMinNumRiders { get; set; }

        [NotMapped]
        public bool isActiveMapping { get; set; }

        public Boolean ysnFlag { get; set; }

        public Boolean ysnCross { get; set; }

        public string strNotes { get; set; }

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