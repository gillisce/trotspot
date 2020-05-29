using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class ClassRegistration
    {
        [Key]
        [Display(Name ="ID")]
        public int intClassRegistrationID { get; set; }

        [Display(Name ="Team ID")]
        public int intHorseRiderID { get; set; }

        [Display(Name ="Class")]
        public int intClassID { get; set; }

    }
}