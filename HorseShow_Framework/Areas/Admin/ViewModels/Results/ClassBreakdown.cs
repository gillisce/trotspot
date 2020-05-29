using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels.Results
{
    public class ClassBreakdown
    {
        public IList<RiderResults> results { get; set; }

        public ClassBreakdown()
        {
            results = new List<RiderResults>();
        }
    }
}