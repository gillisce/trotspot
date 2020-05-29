using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class PointsPerPlace_VM
    {
        [Key]
        public int intPlacePointValues { get; set; }

        [Display(Name = "Place")]
        public int intPlace { get; set; }

        [Display(Name = "Number of Points")]
        public int intNumPoints { get; set; }


        public IList<PlacePointValues> placePointValues { get; set; } 

        public PointsPerPlace_VM()
        {
            placePointValues = new List<PlacePointValues>();
        }
    }
}