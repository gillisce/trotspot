using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Models
{
    public class PlacePointValues
    {
        [Key]
        public int intPlacePointValues { get; set; }

        [Display(Name="Place")]
        public int intPlace { get; set; }

        [Display(Name ="Number of Points")]
        public int intNumPoints { get; set; }

    }
}