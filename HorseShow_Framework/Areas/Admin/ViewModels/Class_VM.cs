using HorseShow_Framework.Models;
using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    [NotMapped]
    public class Class_VM : Models.Class
    {
        public IList<Class> classes { get; set; }
        public PermissionSettings UserPermissions { get; set; }
        public Class_VM()
        {
            classes = new List<Class>();
        }
    }
}