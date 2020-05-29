using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class Person
    {
        [Key]
        [Display(Name = "Person ID")]
        public int intPersonID { get; set; }


        [Display(Name = "First Name")]
        [Required]
        public string strFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string strLastName { get; set; }

        [Display(Name = "Age")]
        [Required]
        public int? intAge { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string intPhoneNumber { get; set; }

        [Display(Name ="Email")]
        [Required]
        [EmailAddress]
        public string strEmail { get; set; }

        [Display(Name ="Address")]
        public string strAddress { get; set; }

        [Display(Name ="City")]
        public string strCity { get; set; }

        [Display(Name ="State")]
        public string strState { get; set; }

        [Display(Name ="Zip Code")]
        public int? intZipCode { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }
    }

}