using HorseShow_Framework.DAO;
using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class AdminShowbill_VM
    {
        private ShowDBContext db = new ShowDBContext();
        private ShowDAL show_DAL = new ShowDAL();

        public Division_VM _division_vm { get; set; }

        public Class_VM _class_vm { get; set; }
        
        public PointsPerPlace_VM _pointsPerPlace_vm { get; set; }
        
        public Store_VM _store_vm { get; set; }
        
        public DivClassMappings_VM _divClassMappings_vm { get; set; }

        public PermissionSettings UserPermissions { get; set; }

        public AdminShowbill_VM()
        {
            _division_vm = new Division_VM();
            _class_vm = new Class_VM();
            _pointsPerPlace_vm = new PointsPerPlace_VM();
            _store_vm = new Store_VM();
            _divClassMappings_vm = new DivClassMappings_VM();
        }
    }
}