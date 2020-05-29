using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.Areas.Admin.ViewModels
{
    public class Store_VM
    {
        [Key]
        [Display(Name = "Item ID")]
        public int intItemID { get; set; }

        [Display(Name = "Item Name")]
        public string strItemName { get; set; }

        [Display(Name = "Item Description")]
        public string strItemDescription { get; set; }

        [Display(Name = " Item Cost")]
        [DataType(DataType.Currency)]
        public float? fltCost { get; set; }

        [Display(Name = "Is Active?")]
        public bool yesnActive { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }

        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        public IList<StoreItems> storeItems { get; set; }

        public Store_VM()
        {
            storeItems = new List<StoreItems>();
        }
    }
}