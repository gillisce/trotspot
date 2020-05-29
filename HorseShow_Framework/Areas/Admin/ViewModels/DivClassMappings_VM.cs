using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class DivClassMappings_VM
    {
       public List<Class> Classes { get; set; }

        public List<Division> Divisions { get; set; }

        public int intDivCount { get; set; }

        public List<DivClassMapping_ByDiv> _ByDivs { get; set; }

        public string stringMapping { get; set; }
        public IEnumerable<DivisionClassMapping> DivisionClassMappings {get; set;}
        public DivClassMappings_VM()
        {
            _ByDivs = new List<DivClassMapping_ByDiv>();
        }
    }


    public class DivClassMapping_ByDiv
    {
        public int intDivisionID { get; set; }

        public IList<mappClasses> classList { get; set; }

        public DivClassMapping_ByDiv()
        {
            classList = new List<mappClasses>();
        }
    }
    public class mappClasses
    {
        public int intClassID { get; set; }
        public string strClassName { get; set; }
    }
}