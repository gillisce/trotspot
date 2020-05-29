using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class HorseRiderScores
    {
        [Key]
        [Display(Name ="ID")]
        public int intHorseRiderScores { get; set; }

        [Display(Name ="Team Id")]
        public int intHorseRiderID { get; set; }

        [Display(Name ="Score")]
        public int intOverallScore { get; set; }

        [Display(Name ="Number of 1st Place Finishes")]
        public int? intNum1stPlace { get; set; }

        [Display(Name = "Number of 2nd Place Finishes")]
        public int? intNum2ndPlace { get; set; }

        [Display(Name = "Number of 3rd Place Finishes")]
        public int? intNum3rdPlace { get; set; }

        [Display(Name = "Number of 4th Place Finishes")]
        public int? intNum4thPlace { get; set; }

        [Display(Name = "Number of 5th Place Finishes")]
        public int? intNum5thPlace { get; set; }
    }
}