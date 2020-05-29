using HorseShow_Framework.Areas.Admin.ViewModels;
using HorseShow_Framework.Areas.Admin.ViewModels.Analytics;
using HorseShow_Framework.Areas.Admin.ViewModels.Results;
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
using System.Web.Script.Serialization;

namespace HorseShow_Framework.Areas.Admin.Controllers
{
    public class RunningShowController : Controller
    {
        readonly SetUpPermissions setUpPermissions = new SetUpPermissions();
        private ShowDBContext db = new ShowDBContext();
        private ShowDAL show_DAL = new ShowDAL();
        private ScoringDAL score_DAL = new ScoringDAL();
        private Analytics_DAL analytics_DAL = new Analytics_DAL();
        // GET: Admin/RunningShow

        public ActionResult ListRegisteredRiders()
        {
            string usr = User.Identity.GetUserName();
            RegisteredRider_VM model = new RegisteredRider_VM()
            {
                listRiders = show_DAL.GetRegisteredRiders().ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };

            return View(model);
        }

        public ActionResult ClassLineUps()
        {
            string usr = User.Identity.GetUserName();
            Class_VM model = new Class_VM()
            {
                classes = db.Classes.ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult getRidersInClass(int intClassNumber)
        {
            ClassRiders model = new ClassRiders()
            {
                intClassNum = intClassNumber,
                strClassName = show_DAL.GetClassNameByNumber(intClassNumber),
                classRiders = show_DAL.GetClassRiders(intClassNumber).ToList(),
            };

            return PartialView("_ClassLineUp", model);
        }

        [HttpPost]
        public ActionResult GetClasses(string id)
        {
            string usr = User.Identity.GetUserName();
            Registration_Class_VM _Class_VM = new Registration_Class_VM()
            {

                lstClasses = show_DAL.GetClasses(id).OrderBy(x => x.intClassNumber).ToList()
            };
            // return Json(_Class_VM, JsonRequestBehavior.AllowGet);
            return PartialView("_EditRiderClassList", _Class_VM);
        }

        public ActionResult EditRiderRegistration(int intHorseRiderID)
        {
            string usr = User.Identity.GetUserName();
            EditRiderRegistration_VM model = new EditRiderRegistration_VM()
            {
                //person = show_DAL.GetPerson(intHorseRiderID),
                horse = show_DAL.GetHorseRider(intHorseRiderID),
                lstSelectedClasses = show_DAL.GetClassRegistrations(intHorseRiderID).ToList(),
                lstSlectedAddOns = show_DAL.GetRiderAddOns(intHorseRiderID).ToList(),
                divisions = db.Divisions.ToList(),
                storeItems = db.StoreItems.ToList(),
                lstExistingRiderNumbers = show_DAL.LstRiderNumbers().ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            model.person = show_DAL.GetPerson(model.horse.intPersonID);
            model.strDivName = show_DAL.GetDivisionName(model.horse.intDivisionID);
            model.oldDivName = show_DAL.GetDivisionName(model.horse.intDivisionID);

            return View("EditRegistration",model);
        }

        [HttpPost]
        public ActionResult EditRiderRegistration(EditRiderRegistration_VM collection)
        {
            try
            {
                //Always run the updates for Person and Rider/Horse Combo
                collection.horse.intDivisionID = show_DAL.GetDivsionIDByName(collection.strDivName);
                int intPersonID = show_DAL.CreateUpdatePersonInfo(collection.person, "U");
                collection.horse.ysnActive = true;
                int intHorseRiderID = show_DAL.CreateUpdateHorseRider(collection.horse, "U");
                double totDue = collection.horse.fltAmountDue;
                //If the store List was edited, wipe out old selections and reinsert
                if(collection.strSelectedStoreItems != "NoEdit")
                {
                    show_DAL.DeleteRiderAddOns(intHorseRiderID);
                    if (!string.IsNullOrEmpty(collection.strSelectedStoreItems) && collection.strSelectedStoreItems != "NoEdit")
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
                        }
                    }
                }

                if(collection.strDivName != collection.oldDivName || collection.strSelectedClasses != "NoEdit")
                {
                    show_DAL.DeleteRiderClassRegistrations(intHorseRiderID);
                    if (!string.IsNullOrEmpty(collection.strSelectedClasses) && collection.strSelectedClasses != "NoEdit")
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
                        }
                    }
                }

                return Json(new { message = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { message = "fail" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult PlaceClass()
        {
            string usr = User.Identity.GetUserName();
            Class_VM model = new Class_VM()
            {

                classes = db.Classes.ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            return View(model);
        }

        public ActionResult getPossiblePlacing(int intClassNumber)
        {
            PlaceClass_VM model = new PlaceClass_VM()
            {
                intClassNumber = intClassNumber,
                riders = show_DAL.GetClassRiders(intClassNumber).ToList(),
                places = db.PointValues.ToList(),
                intClassID = show_DAL.GetClassIDByNumber(intClassNumber)
            };
            model.existingPlaces = score_DAL.GetCurrentPlacings(model.intClassID);
            if (model.existingPlaces.Count == 0)
                model.newPlacing = true;
            else
                model.newPlacing = false;
            return PartialView("_ClassPlacingPartial", model);
        }

        [HttpPost]
        public ActionResult SubmitPlacing(PlaceClass_VM collection)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                RiderPlacings_JSON[] placings = js.Deserialize<RiderPlacings_JSON[]>(collection.strPlacedRiders);
                string AddUpdateFlag;
                Console.Write("BREAK");
                IList<ExistingRiderPlacing> existingPlaces = score_DAL.GetCurrentPlacings(collection.intClassID);

                if (collection.newPlacing)
                    AddUpdateFlag = "I";
                else
                    AddUpdateFlag = "U";

                foreach (var rider in placings)
                {
                    bool skipOverallUpdate = false;
                    ClassPlacing tmpObj = new ClassPlacing()
                    {
                        intClassID = collection.intClassID,
                        intHorseRiderID = rider.intHorseRiderID,
                        intPlace = rider.intPlace
                    };
                    // Check if Rider was already placed -- Only Valid for Updated Placings
                    if(AddUpdateFlag == "U")
                    {
                        bool alreadyPlaced = existingPlaces.Any(item => item.intPlace == tmpObj.intPlace && item.intHorseRiderID == tmpObj.intHorseRiderID);
                        if (alreadyPlaced)
                            skipOverallUpdate = true;
                        else
                        {
                            skipOverallUpdate = false;
                            int placeToChage = tmpObj.intPlace;
                            SubtractRiderOverall(existingPlaces, placeToChage);
                        }
                    }
                    score_DAL.AddUpdateClassPlacing(tmpObj, AddUpdateFlag);

                    int points = score_DAL.GetPointsForPlace(tmpObj.intPlace);

                    if (!skipOverallUpdate) //Only want to do fresh counter if they haven't been placed in the position for the class
                    {
                        HorseRiderScores current = score_DAL.GetHorseRiderScores(rider.intHorseRiderID);
                        string riderInsert;
                        Console.Write("BREAK");

                        if (current.intHorseRiderID == 0)
                            riderInsert = "I";
                        else
                            riderInsert = "U";
                        HorseRiderScores updatedScore = new HorseRiderScores()
                        {
                            intHorseRiderID = rider.intHorseRiderID,
                            intOverallScore = current.intOverallScore + points,
                            intNum1stPlace = current.intNum1stPlace,
                            intNum2ndPlace = current.intNum2ndPlace,
                            intNum3rdPlace = current.intNum3rdPlace,
                            intNum4thPlace = current.intNum4thPlace,
                            intNum5thPlace = current.intNum5thPlace
                        };
                        if (rider.intPlace == 1)
                            updatedScore.intNum1stPlace = ++updatedScore.intNum1stPlace ?? 1;
                        else if (tmpObj.intPlace == 2)
                            updatedScore.intNum2ndPlace = ++updatedScore.intNum2ndPlace ?? 1;
                        else if (tmpObj.intPlace == 3)
                            updatedScore.intNum3rdPlace = ++updatedScore.intNum3rdPlace ?? 1;
                        else if (tmpObj.intPlace == 4)
                            updatedScore.intNum4thPlace = ++updatedScore.intNum4thPlace ?? 1;
                        else if (tmpObj.intPlace == 5)
                            updatedScore.intNum5thPlace = ++updatedScore.intNum5thPlace ?? 1;

                        string message = score_DAL.AddUpdateHorseRiderScores(updatedScore, riderInsert);
                    }
                }
                return Json(new { message = "success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = "Fail" + ex.Message }, JsonRequestBehavior.AllowGet);

            }



        }

        public ActionResult BestInShow()
        {
            string usr = User.Identity.GetUserName();
            List<Division> divs = db.Divisions.ToList();
            List<RiderResults> unsorted = score_DAL.GetBestInShow(divs).ToList();
            BestInShow model = new BestInShow()
            {
                results = unsorted.OrderByDescending(order => order.intTotalScore).ThenBy(order => order.int1stPlaceCount).ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            return View("Results/BestInShow", model);
        }

        public ActionResult DivisionScores()
        {
            string usr = User.Identity.GetUserName();
            DivisionBreakDown pageVM = new DivisionBreakDown()
            {
                divs = db.Divisions.OrderBy(or => or.intMaxAge).ThenBy(or => or.strDivisionName).ToList(),
                results = score_DAL.GetDivisionBreakDowns().OrderByDescending(or => or.intTotalScore).ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            return View("Results/Scores_DivisionBreakdown", pageVM);
        }

        public ActionResult ClassScores()
        {
            string usr = User.Identity.GetUserName();
            Class_VM model = new Class_VM()
            {
                classes = db.Classes.ToList(),
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)

            };
            return View("Results/Scores_ClassListing", model);
        }

        [HttpPost]
        public ActionResult getScoresInClass(int intClassNumber)
        {
            ClassBreakdown model = new ClassBreakdown()
            {
                results = score_DAL.GetClassScores(intClassNumber)
            };

            return PartialView("Results/_ClassScoreList", model);
        }

        [HttpPost]
        public ActionResult RemoveRider(int intHorseRiderID)
        {
            string message;
            try
            {
                HorseRider removed = new HorseRider()
                {
                    intHorseRiderID = intHorseRiderID,
                    ysnActive = false,
                    ysnCheckedIn = false,
                    ysnHasPaid = false
                };
                show_DAL.RemoveRider(removed);
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewAllRiderData()
        {
            string usr = User.Identity.GetUserName();
            ExportRiderInfo_Vm model = new ExportRiderInfo_Vm()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            model.Data = analytics_DAL.GetAllRiders();
            return View("DataExports/ExportRiderInfo", model);
        }

        public ActionResult ExportPlacingsClass()
        {
            string usr = User.Identity.GetUserName();
            Export_ClassPlacings model = new Export_ClassPlacings()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            model.Data = analytics_DAL.Get_ClassPlacings();
            return View("DataExports/ExportClassPlacing", model);
        }

        public ActionResult ExportClassLineUps()
        {
            string usr = User.Identity.GetUserName();
            Export_ClassLineUps model = new Export_ClassLineUps()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            model.classLines_Registered = analytics_DAL.GetClassLineUpsExport();
            model.classLines_CheckedIn = model.classLines_Registered.Where(x => x.CheckedIn).ToList();

            return View("DataExports/ExportClassLineUps", model);
        }

        public ActionResult ExportMasterScoreSheet()
        {
            string usr = User.Identity.GetUserName();
            Export_MasterScoreSheet model = new Export_MasterScoreSheet()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            model.Data = analytics_DAL.GetMasterScore();

            return View("DataExports/ExportMasterScoreSheet", model);
        }

        public ActionResult GetShowStats()
        {
            string usr = User.Identity.GetUserName();
            RiderStats model = new RiderStats()
            {
                UserPermissions = setUpPermissions.SetGlobalUserPermissions(usr)
            };
            model.generalInfo = analytics_DAL.GetGeneralRiderData();
            model.ShowStats = analytics_DAL.GetShowStats();

            return View("Analytics/RiderStats", model);
        }


        private void SubtractRiderOverall(IList<ExistingRiderPlacing> existingPlaces, int placeToChage)
        {
            var riderToChange = existingPlaces.FirstOrDefault(r => r.intPlace == placeToChage);
            int points1 = score_DAL.GetPointsForPlace(placeToChage);
            HorseRiderScores current = score_DAL.GetHorseRiderScores(riderToChange.intHorseRiderID);
            Console.Write("BREAK");

            string riderInsert;
             

            if (current.intHorseRiderID == 0)
                riderInsert = "I";
            else
                riderInsert = "U";

            HorseRiderScores updatedScore = new HorseRiderScores()
            {
                intHorseRiderID = riderToChange.intHorseRiderID,
                intOverallScore = current.intOverallScore - points1,
                intNum1stPlace = current.intNum1stPlace,
                intNum2ndPlace = current.intNum2ndPlace,
                intNum3rdPlace = current.intNum3rdPlace,
                intNum4thPlace = current.intNum4thPlace,
                intNum5thPlace = current.intNum5thPlace
            };
            if (placeToChage == 1)
                updatedScore.intNum1stPlace -= 1;
            else if (placeToChage == 2)
                updatedScore.intNum2ndPlace -= 1;
            else if (placeToChage == 3)
                updatedScore.intNum3rdPlace -= 1;
            else if (placeToChage == 4)
                updatedScore.intNum4thPlace -= 1;
            else if (placeToChage == 5)
                updatedScore.intNum5thPlace -= 1;

            string message = score_DAL.AddUpdateHorseRiderScores(updatedScore, riderInsert);
        }
    }
}