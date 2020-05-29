using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class HorseRider
    {
        [Key]
        [Display(Name="Team ID")]
        public int intHorseRiderID { get; set; }

        [Display(Name ="Person ID")]
        public int intPersonID { get; set; }

        [Display(Name ="Horse Name")]
        [Required]
        public string strHorseName { get; set; }

        [Display(Name ="Rider Number")]
        public int? intRiderNumber { get; set; }

        [Display(Name ="Is Checked In")]
        public bool ysnCheckedIn { get; set; }

        [Display(Name ="Amount Due")]
        public float fltAmountDue { get; set; }

        [Display(Name ="Has Paid")]
        public bool ysnHasPaid { get; set; }

        [Display(Name ="Payment Method")]
        public string strPaymentMethod { get; set; }

        [Display(Name = "Division ID")]
        public int intDivisionID { get; set; }

        public bool ysnActive { get; set; }
         
    }
}