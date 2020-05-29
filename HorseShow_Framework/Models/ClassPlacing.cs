using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class ClassPlacing
    {
        [Key]
        [Display(Name ="ID")]
        public int intClassPlacing { get; set; }

        [Display(Name ="Team Id")]
        public int intHorseRiderID { get; set; }

        [Display(Name ="Class ID")]
        public int intClassID { get; set; }

        [Display(Name ="Place")]
        public int intPlace { get; set; }
    }
}