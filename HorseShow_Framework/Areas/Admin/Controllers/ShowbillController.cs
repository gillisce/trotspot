using HorseShow_Framework.Areas.Admin.ViewModels;
using HorseShow_Framework.DAO;
using HorseShow_Framework.HelperClasses;
using HorseShow_Framework.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HorseShow_Framework.Areas.Admin.Controllers
{ 
    public class ShowbillController : Controller
    {
        readonly SetUpPermissions setUpPermissions = new SetUpPermissions();
        private ShowDBContext db = new ShowDBContext();
        private ShowDAL show_DAL = new ShowDAL();
        // GET: Admin/Showbill
        public ActionResult Index()
        {
            AdminShowbill_VM model = new AdminShowbill_VM();
            model._division_vm.divisions = db.Divisions.ToList();
            model._class_vm.classes = db.Classes.ToList();
            model._pointsPerPlace_vm.placePointValues = db.PointValues.ToList();
            model._store_vm.storeItems = db.StoreItems.ToList();
            model._divClassMappings_vm.Classes = db.Classes.OrderBy(x => x.intClassNumber).ToList();
            model._divClassMappings_vm.Divisions = db.Divisions.OrderBy(x => x.intMinAge).ToList();
            model._divClassMappings_vm.DivisionClassMappings = db.DivisionClassMappings.OrderBy(x => x.intDivisionID).ToList();
            model.UserPermissions = setUpPermissions.SetGlobalUserPermissions(User.Identity.GetUserName());

            return View(model);
        }

        public ActionResult ListDivisions()
        {
            Division_VM model = new Division_VM()
            {
                divisions = db.Divisions.ToList()
            };
            return PartialView("Division_Index", model);
        }

        public ActionResult ListClasses()
        {
            Class_VM model = new Class_VM()
            {
                classes = db.Classes.ToList()
            };
            return PartialView("Class_Index", model);
        }

        public ActionResult ListPointsPerPlace()
        {
            PointsPerPlace_VM model = new PointsPerPlace_VM()
            {
                placePointValues = db.PointValues.ToList()
            };

            return PartialView("PlacePoint_Index", model);
        }

        public ActionResult ListStore()
        {
            Store_VM model = new Store_VM()
            {
                storeItems = db.StoreItems.ToList()
            };

            return PartialView("Store_Index", model);
        }

        #region Division Create/Remove/Update
        [HttpPost]
        public ActionResult CreateDivision(string strDivisionName, string strDivisionDescr, int intMinRiderAge, int intMaxRiderAge)
        {
            string message;
            try {
                Division newDiv = new Division()
                {
                    strDivisionName = strDivisionName,
                    strDivisionDescr = strDivisionDescr,
                    intMinAge = intMinRiderAge,
                    intMaxAge = intMaxRiderAge,
                    dtmCreated = DateTime.Now,
                    strCreatorID = "Admin"
                };
                db.Divisions.Add(newDiv);
                db.SaveChanges();
               message  = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            } catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RemoveDivision(int intDivisionID)
        {
            string message;
            try
            {
                Division removedDiv = new Division()
                {
                    intDivisionID = intDivisionID,
                    ysnActive = false
                };
                db.Entry(removedDiv).State = EntityState.Modified;
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: UseCase/Admin/Edit/5
        [HttpPost]
        public ActionResult UpdateDivision(int id, string strDivisionName, string strDivisionDescr, int intMinRiderAge, int intMaxRiderAge)
        {
            string message;
            try
            {
                Division modDiv = new Division()
                {
                    intDivisionID = id,
                    strDivisionName = strDivisionName,
                    strDivisionDescr = strDivisionDescr,
                    intMinAge = intMinRiderAge,
                    intMaxAge = intMaxRiderAge,
                    dtmModified = DateTime.Now,
                    strModifierID = "Admin"
                };
                db.Entry(modDiv).State = EntityState.Modified;
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Class Create/Remove/Update
        [HttpPost]
        public ActionResult CreateClass(int intClassNumber, string strClassName, string strNotes, int fltCost, bool ysnFlag = false, bool ysnCross = false)
        {
            string message;
            try
            {
                Class @class = new Class()
                {
                    intClassNumber = intClassNumber,
                    strClassName = strClassName,
                    fltCost = fltCost,
                    strNotes = strNotes,
                    ysnFlag = ysnFlag,
                    ysnCross = ysnCross,
                    dtmCreated = DateTime.Now,
                    strCreatorID = "Admin"
                };
                string addUpdateFlag = "I";
                show_DAL.AddUpdateClass(@class, addUpdateFlag);

                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RemoveClass(int intClassID)
        {
            string message;
            try
            {
                Class @class = db.Classes.Find(intClassID);
                db.Classes.Remove(@class);
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: UseCase/Admin/Edit/5
        [HttpPost]
        public ActionResult UpdateClass(int id, int intClassNumber, string strClassName, string strNotes, int fltCost, bool ysnFlag = false, bool ysnCross = false)
        {
            string message;
            try
            {
                Class modClass = new Class()
                {
                    intClassID = id,
                    intClassNumber = intClassNumber,
                    strClassName = strClassName,
                    fltCost = fltCost,
                    strNotes = strNotes,
                    ysnFlag = ysnFlag,
                    ysnCross = ysnCross,
                    dtmModified = DateTime.Now,
                    strModifierID = "Admin"
                };
                string addUpdateFlag = "U";
                show_DAL.AddUpdateClass(modClass, addUpdateFlag);
                show_DAL.UpdateDivClassMapping(modClass);


                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Points Per Placing Create/Remove/Update
        [HttpPost]
        public ActionResult CreatePointsPerPlace(int intPlace, int intPointValue)
        {
            string message;
            try
            {
                PlacePointValues placePointValues = new PlacePointValues()
                {
                    intPlace = intPlace,
                    intNumPoints = intPointValue
                };
                db.PointValues.Add(placePointValues);
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RemovePointsPerPlace(int intPlacePointValues)
        {
            string message;
            try
            {
                PlacePointValues @ppv = db.PointValues.Find(intPlacePointValues);
                db.PointValues.Remove(@ppv);
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: UseCase/Admin/Edit/5
        [HttpPost]
        public ActionResult UpdatePointsPerPlace(int intPlacePointValues, int intPlace, int intPointValue)
        {
            string message;
            try
            {
                PlacePointValues placePointValues = new PlacePointValues()
                {
                    intPlacePointValues = intPlacePointValues,
                    intPlace = intPlace,
                    intNumPoints = intPointValue
                };
                db.Entry(placePointValues).State = EntityState.Modified;
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region Store Create/Remove/Update
        [HttpPost]
        public ActionResult CreateStoreItem(string strItemName, string strItemDescr, int fltCost)
        {
            string message;
            try
            {
                StoreItems storeItems = new StoreItems()
                {
                    strItemName = strItemName,
                    strItemDescription = strItemDescr,
                    fltCost = fltCost,
                    dtmCreated = DateTime.Now,
                    strCreatorID = "Admin"
                };
                db.StoreItems.Add(storeItems);
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RemoveStoreItem(int storeItemID)
        {
            string message;
            try
            {
                StoreItems storeItems = db.StoreItems.Find(storeItemID);
                db.StoreItems.Remove(storeItems);
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: UseCase/Admin/Edit/5
        [HttpPost]
        public ActionResult UpdateStoreItem(int storeItemID, string strItemName, string strItemDescr, int fltCost)
        {
            string message;
            try
            {
                StoreItems storeItems = new StoreItems()
                {
                    strItemName = strItemName,
                    strItemDescription = strItemDescr,
                    fltCost = fltCost,
                    dtmModified = DateTime.Now,
                    strCreatorID = "Admin"
                };
                db.Entry(storeItems).State = EntityState.Modified;
                db.SaveChanges();
                message = "Success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult ListDivClassMap()
        {
            var model = new DivClassMappings_VM()
            {
                Classes = db.Classes.OrderBy(x => x.intClassNumber).ToList(),
                Divisions = db.Divisions.OrderBy(x=> x.intMinAge).ToList(),
                DivisionClassMappings = db.DivisionClassMappings.OrderBy(x=> x.intDivisionID).ToList()
            };
            model.intDivCount = model.Divisions.Count();

            return PartialView("DivClass_Index", model);
        }

        // POST: DivisionClassMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult DivClass_Index(DivClassMappings_VM collection)
        {
            string obj = collection.stringMapping;
            JavaScriptSerializer js = new JavaScriptSerializer();
            DivClassMapping_ByDiv[] byDiv = js.Deserialize<DivClassMapping_ByDiv[]>(obj);
            List<DivisionClassMapping> oldMap = db.DivisionClassMappings.OrderBy(x => x.intDivisionID).ToList();

            DivisionClassMapping classMapper;

            Console.Write(collection);

            foreach(var div in byDiv)
            {
                var tmpClassList = oldMap.Where(c => c.intDivisionID == div.intDivisionID).Select(a => new{ a.intClassID, a.strClassName, a.dtmCreated, a.ysnActive }).ToList();
                Console.Write("BREAK");

                var newClasses = div.classList.Where(c => tmpClassList.All(c2 => c2.intClassID != c.intClassID));
                var reactivateClass = div.classList.Where(c => tmpClassList.Any(c2 => c2.intClassID == c.intClassID && !c2.ysnActive));
                var deletedClassList = tmpClassList.Where(c => div.classList.All(c2 => c2.intClassID != c.intClassID));

                Console.Write("BREAK");
                foreach(var classReactivate in reactivateClass)
                {
                    classMapper = new DivisionClassMapping()
                    {
                        intDivisionID = div.intDivisionID,
                        strDivisionName = show_DAL.GetDivisionName(div.intDivisionID),
                        intClassID = classReactivate.intClassID,
                        strClassName = classReactivate.strClassName,
                        dtmModified = DateTime.Now,
                        strModifierID = "Show Admin",
                        ysnActive = true
                    };
                    show_DAL.SetClassMappingActive(classMapper);
                }
                foreach (var classToAdd in newClasses)
                {
                    classMapper = new DivisionClassMapping()
                    {
                        intDivisionID = div.intDivisionID,
                        strDivisionName = show_DAL.GetDivisionName(div.intDivisionID),
                        intClassID = classToAdd.intClassID,
                        strClassName = classToAdd.strClassName,
                        dtmCreated = DateTime.Now,
                        strCreatorID = "Show Admin",
                        ysnActive = true
                    };
                    db.DivisionClassMappings.Add(classMapper);
                    db.SaveChanges();
                }
                foreach (var classToDelete in deletedClassList)
                {
                    classMapper = new DivisionClassMapping()
                    {
                        intDivisionID = div.intDivisionID,
                        intClassID = classToDelete.intClassID,
                        dtmModified = DateTime.Now,
                        strModifierID = "Show Admin",
                        ysnActive = false
                    };
                    show_DAL.SetClassMappingInactive(classMapper);
                }
            }
            return Json(new { message = "success" }, JsonRequestBehavior.AllowGet);
        }













    }

}