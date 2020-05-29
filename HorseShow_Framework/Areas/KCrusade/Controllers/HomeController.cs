using HorseShow_Framework.Areas.KCrusade.ViewModels;
using HorseShow_Framework.DAO;
using HorseShow_Framework.HelperClasses;
using HorseShow_Framework.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorseShow_Framework.Areas.KCrusade.Controllers
{
    public class HomeController : Controller
    {
        readonly SetUpPermissions setUpPermissions = new SetUpPermissions();
        private ShowDBContext db = new ShowDBContext();
        private ShowDAL show_DAL = new ShowDAL();

        // GET: KCrusade/Home
        public ActionResult Index()
        {
            Index_VM @page = new Index_VM()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(User.Identity.GetUserName())
            };
            return View(@page);
        }

        public ActionResult Registration()
        {
            string usr = User.Identity.GetUserName();
            RegistrationMain_VM model = new RegistrationMain_VM()
            {
                divisions = db.Divisions.ToList(),
                storeItems = db.StoreItems.ToList(),
                lstExistingRiderNumbers = show_DAL.LstRiderNumbers().ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Registration(RegistrationMain_VM collection)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    List<string> registrationEmail = new List<string>();
                    int intDivisonID = show_DAL.GetDivsionIDByName(collection.strDivName);
                    int intPersonID = show_DAL.CreateUpdatePersonInfo(collection.person, "I");

                    collection.horse.intDivisionID = intDivisonID;
                    collection.horse.fltAmountDue = collection.totalDue;
                    collection.horse.intPersonID = intPersonID;
                    collection.horse.ysnActive = true;
                    int intHorseRiderID = show_DAL.CreateUpdateHorseRider(collection.horse, "I");

                    if (!string.IsNullOrEmpty(collection.strSelectedClasses))
                    {
                        string[] arrClasses = collection.strSelectedClasses.Split(',');
                        ClassRegistration @class;
                        for (int i = 0; i < arrClasses.Length; i++)
                        {
                            @class = new ClassRegistration()
                            {
                                intClassID = Convert.ToInt32(arrClasses[i]),
                                intHorseRiderID = intHorseRiderID
                            };
                            show_DAL.CreateUpdateClassRegistration(@class);
                            registrationEmail.Add(show_DAL.GetClassInfo_Email(@class.intClassID));
                        }
                    }
                    if (!string.IsNullOrEmpty(collection.strSelectedStoreItems))
                    {
                        string[] arrStore = collection.strSelectedStoreItems.Split(',');
                        RiderAddOns @rider;
                        for (int i = 0; i < arrStore.Length; i++)
                        {
                            @rider = new RiderAddOns()
                            {
                                intHorseRiderID = intHorseRiderID,
                                intStoreItemID = Convert.ToInt32(arrStore[i])
                            };
                            show_DAL.CreateUpdateRiderAddOns(@rider);
                            registrationEmail.Add(show_DAL.GetStoreItme_Email(@rider.intStoreItemID));
                        }

                    }
                    HelperClasses.Email.GenerateEmailContent(collection.person, collection.horse, registrationEmail, collection.strDivName);
                    //TempData["shortMessage"] = "success";
                    return Json(new { message = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return Json(new { message = "MissingFields" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { message = "Fail" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
           // RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult GetClasses(string id)
        {
            List<Class> unsorted = show_DAL.GetClasses(id).ToList();
            Registration_Class_VM _Class_VM = new Registration_Class_VM()
            {
                lstClasses = unsorted.OrderBy(order => order.intClassNumber).ToList()
            };
            // return Json(_Class_VM, JsonRequestBehavior.AllowGet);
            return PartialView("_ClassList", _Class_VM);
        }



    }
}
