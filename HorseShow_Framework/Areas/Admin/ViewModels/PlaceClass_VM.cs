using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class PlaceClass_VM
    {
        public IList<ClassRiders> riders { get; set; }

        public IList<PlacePointValues> places { get; set; }

        public IList<ExistingRiderPlacing> existingPlaces { get; set; }

        public bool newPlacing { get; set; }

        public int intClassID { get; set; }

        public int intClassNumber { get; set; }

        public string strPlacedRiders { get; set; }
        public PlaceClass_VM()
        {
            riders = new List<ClassRiders>();
            places = new List<PlacePointValues>();
            existingPlaces = new List<ExistingRiderPlacing>();
        }
    }


    public class RiderPlacings_JSON
    {
        public int intHorseRiderID { get; set; }
        public int intPlace { get; set; }
    }

    public class ExistingRiderPlacing
    {
        public int intHorseRiderID { get; set; }
        public int? intClassID { get; set; }
        public int? intPlace { get; set; }
        public int? intRiderNumber { get; set; }
    }
}