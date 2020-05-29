using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class RiderAddOns
    {
        [Key]
        [Display(Name ="Rider Add On ID")]
        public int intRiderAddOnsID { get; set; }

        [Display(Name = "Store Item ID")]
        public int intStoreItemID { get; set; }

        [Display(Name ="Team ID")]
        public int intHorseRiderID { get; set; }
    }
}