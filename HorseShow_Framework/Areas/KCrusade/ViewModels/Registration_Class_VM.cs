using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.KCrusade.ViewModels
{
    public class Registration_Class_VM
    {
        public IList<Class> lstClasses { get; set; }

        public bool isSelected { get; set; }
    }
}