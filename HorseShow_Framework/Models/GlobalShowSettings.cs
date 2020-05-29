using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class GlobalShowSettings
    {
        [Key]
        public int intGlobalShowSettingsID { get; set; }

        [Display(Name = "Parameter Name")]
        public string strParamName { get; set; }

        [Display(Name ="Parameter Description")]
        public string strParamDescr { get; set; }

        [Display(Name ="Parameter Value")]
        public int? intParameterValue { get; set; }

        [Display(Name = "Is Active?")]
        public bool intYsNActive { get; set; }

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