using HorseShow_Framework.Areas.Admin.ViewModels;
using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.DAO
{
    public class ShowDAL
    {
        public IEnumerable<Class> GetClasses(string divsionName)
        {
            List<Class> _retVal = new List<Class>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            Class tmpClass = new Class();

            ConnectionString(objCon);

            objCom = new SqlCommand("select c.intClassID, c.intClassNumber, dc.strClassName, c.fltCost, c.strNotes, c.ysnFlag, c.ysnCross, dc.ysnActive from dbo.Class c, dbo.DivisionClassMapping dc " +
                "where dc.intClassID = c.intClassID and dc.strDivisionName = @strDivisionName", objCon);
            objCom.Parameters.Add(new SqlParameter("@strDivisionName", divsionName));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpClass = new Class();
                tmpClass.intClassID = Convert.ToInt32(reader["intClassID"]);
                tmpClass.intClassNumber = Convert.ToInt32(reader["intClassNumber"]);
                tmpClass.strClassName = reader["strClassName"].ToString();
                tmpClass.strNotes = reader["strNotes"].ToString();
                tmpClass.isActiveMapping = Convert.ToBoolean(reader["ysnActive"]);
                tmpClass.ysnFlag = Convert.ToBoolean(reader["ysnFlag"]);
                tmpClass.ysnCross = Convert.ToBoolean(reader["ysnCross"]);
                tmpClass.fltCost = Convert.ToInt32(reader["fltCost"]);
                _retVal.Add(tmpClass);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public int GetDivsionIDByName(string DivName)
        {
            int _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select intDivisionID from dbo.division  where strDivisionName = @strDivisionName", objCon);
            objCom.Parameters.Add(new SqlParameter("@strDivisionName", DivName));
            objCon.Open();
            _retVal = Convert.ToInt32(objCom.ExecuteScalar());
            objCon.Close();
            return _retVal;
        }

        public int GetClassCount( int horseRiderID)
        {
            int _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select Count(1) from ClassRegistration where intHorseRiderID = @id", objCon);
            objCom.Parameters.Add(new SqlParameter("@id", horseRiderID));
            objCon.Open();
            _retVal = Convert.ToInt32(objCom.ExecuteScalar());
            objCon.Close();
            return _retVal;
        }

        public string GetClassNameByNumber(int ClassNumber)
        {
            string _retVal = "";
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select strClassName from dbo.class  where intClassNumber = @intClassNum", objCon);
            objCom.Parameters.Add(new SqlParameter("@intClassNum", ClassNumber));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                _retVal = reader["strClassName"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string GetClassInfo_Email(int intClassID)
        {
            string _retVal = "";
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select intClassNumber, strClassName from dbo.class  where intClassID = @id", objCon);
            objCom.Parameters.Add(new SqlParameter("@id", intClassID));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                _retVal = Convert.ToInt32(reader["intClassNumber"]) + ": " + reader["strClassName"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }


        public string GetStoreItme_Email(int storeItemID)
        {
            string _retVal = "";
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select strItemName from dbo.StoreItems  where intItemID = @id", objCon);
            objCom.Parameters.Add(new SqlParameter("@id", storeItemID));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                _retVal = reader["strItemName"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }


        public int GetClassIDByNumber(int ClassNumber)
        {
            int _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select intClassID from dbo.class  where intClassNumber = @intClassNum", objCon);
            objCom.Parameters.Add(new SqlParameter("@intClassNum", ClassNumber));
            objCon.Open();
            _retVal = Convert.ToInt32(objCom.ExecuteScalar());
            objCon.Close();
            return _retVal;
        }


        public int CreateUpdatePersonInfo(Person person, string AddUpdateFlag)
        {
            int _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.CreateUpdatePerson", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intPersonID", ToDBNull(person.intPersonID)));
                objCom.Parameters.Add(new SqlParameter("@strFirstName", ToDBNull(person.strFirstName)));
                objCom.Parameters.Add(new SqlParameter("@strLastName", ToDBNull(person.strLastName)));
                objCom.Parameters.Add(new SqlParameter("@intAge", ToDBNull(person.intAge)));
                objCom.Parameters.Add(new SqlParameter("@intPhoneNumber", ToDBNull(person.intPhoneNumber)));
                objCom.Parameters.Add(new SqlParameter("@strEmail", ToDBNull(person.strEmail)));
                objCom.Parameters.Add(new SqlParameter("@strCity", ToDBNull(person.strCity)));
                objCom.Parameters.Add(new SqlParameter("@strAddress", ToDBNull(person.strAddress)));
                objCom.Parameters.Add(new SqlParameter("@strState", ToDBNull(person.strState)));
                objCom.Parameters.Add(new SqlParameter("@intZipCode", ToDBNull(person.intZipCode)));

                objCon.Open();
                if(AddUpdateFlag == "I")
                {
                    _retVal = (int)objCom.ExecuteScalar();
                }
                else
                {
                    objCom.ExecuteScalar();
                    _retVal = person.intPersonID;
                }
                objCon.Close();
            }
            catch (Exception ex)
            {
                _retVal = -1;
            }
            return _retVal;

        }

        public int CreateUpdateHorseRider(HorseRider horseRider, string AddUpdateFlag)
        {
            int _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.CreateUpdateHorseRider", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", horseRider.intHorseRiderID));
                objCom.Parameters.Add(new SqlParameter("@intPersonID", ToDBNull(horseRider.intPersonID)));
                objCom.Parameters.Add(new SqlParameter("@strHorseName", ToDBNull(horseRider.strHorseName)));
                objCom.Parameters.Add(new SqlParameter("@intRiderNumber", ToDBNull(horseRider.intRiderNumber)));
                objCom.Parameters.Add(new SqlParameter("@ysnCheckIn", ToDBNull(horseRider.ysnCheckedIn)));
                objCom.Parameters.Add(new SqlParameter("@ysnHasPaid", ToDBNull(horseRider.ysnHasPaid)));
                objCom.Parameters.Add(new SqlParameter("@fltAmountDue", ToDBNull(horseRider.fltAmountDue)));
                objCom.Parameters.Add(new SqlParameter("@strPaymentMethod", ToDBNull(horseRider.strPaymentMethod)));
                objCom.Parameters.Add(new SqlParameter("@intDivisionID", ToDBNull(horseRider.intDivisionID)));
                objCom.Parameters.Add(new SqlParameter("@ysnActive", horseRider.ysnActive));

                objCon.Open();
                if(AddUpdateFlag == "I")
                {
                    _retVal = (int)objCom.ExecuteScalar();
                }
                else
                {
                    objCom.ExecuteScalar();
                    _retVal = horseRider.intHorseRiderID;
                }
                objCon.Close();
            }
            catch (Exception ex)
            {
                _retVal = -1;
            }
            return _retVal;
        }

        public string CreateUpdateRiderAddOns(RiderAddOns riderAddOns)
        {
            string _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.CreateUpdateRiderAddOn", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", ToDBNull(riderAddOns.intHorseRiderID)));
                objCom.Parameters.Add(new SqlParameter("@intStoreItemID", ToDBNull(riderAddOns.intStoreItemID)));
                objCon.Open();
                objCom.ExecuteScalar();
                //_retVal = (int)objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Good";
                objCon.Close();
            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public string CreateUpdateClassRegistration(ClassRegistration classRegistration)
        {
            string _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.CreateUpdateClassRegistration", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", ToDBNull(classRegistration.intHorseRiderID)));
                objCom.Parameters.Add(new SqlParameter("@intClassID", ToDBNull(classRegistration.intClassID)));
                objCon.Open();
                objCom.ExecuteScalar();
                //_retVal = (int)objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Good";
                objCon.Close();
            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public IEnumerable<RegisteredRider_VM> GetRegisteredRiders()
        {
            List<RegisteredRider_VM> _retVal = new List<RegisteredRider_VM>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ConnectionString(objCon);
            RegisteredRider_VM tmpRider;
            objCom = new SqlCommand("select hr.intHorseRiderID, p.strFirstName, p.strLastName, hr.strHorseName, hr.intRiderNumber, hr.fltAmountDue, hr.ysnCheckedIn, hr.ysnHasPaid, hr.strPaymentMethod, d.strDivisionName from dbo.Person p, " +
                " dbo.HorseRider hr, dbo.Division d where p.intPersonID = hr.intPersonID and hr.intDivisionID = d.intDivisionID and hr.ysnActive = 1", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpRider = new RegisteredRider_VM();
                tmpRider.intHorseRiderID = Convert.ToInt32(reader["intHorseRiderID"]);
                tmpRider.strRiderName = reader["strFirstName"].ToString() + " " + reader["strLastName"].ToString();
                tmpRider.strHorseName =reader["strHorseName"].ToString();
                tmpRider.strDivisionRegistered = reader["strDivisionName"].ToString();
                tmpRider.intRiderNumber = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]);
                tmpRider.intTotalDue = Convert.ToInt32(reader["fltAmountDue"]);
                tmpRider.isCheckedIn = Convert.ToBoolean(reader["ysnCheckedIn"]);
                tmpRider.isPaid = Convert.ToBoolean(reader["ysnHasPaid"]);
                tmpRider.intClassCount = DAL_Helpers.getDBIntValue(GetClassCount(tmpRider.intHorseRiderID));

                _retVal.Add(tmpRider);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public IEnumerable<Class> GetClassRegistrations(int horseRiderID)
        {
            List<Class> _retVal = new List<Class>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            Class tmpClassReg;

            ConnectionString(objCon);

            objCom = new SqlCommand("Select c.intClassID, c.fltCost, c.intClassNumber, c.strClassName, c.ysnFlag, c.ysnCross, dc.ysnActive from Class c , ClassRegistration cr, HorseRider hr, DivisionClassMapping dc" +
                                " Where c.intClassID = cr.intClassID" +
                                " and hr.ysnActive = 1" +
                                " and  cr.intHorseRiderID = hr.intHorseRiderID" +
                                " and hr.intDivisionID = dc.intDivisionID and c.intClassID = dc.intClassID" +
                                " and cr.intHorseRiderID = @intHorseRiderID", objCon);
            objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", horseRiderID));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpClassReg = new Class();
                tmpClassReg.intClassID = Convert.ToInt32(reader["intClassID"]);
                tmpClassReg.fltCost = Convert.ToInt32(reader["fltCost"]);
                tmpClassReg.isActiveMapping = Convert.ToBoolean(reader["ysnActive"]);
                tmpClassReg.ysnFlag = Convert.ToBoolean(reader["ysnFlag"]);
                tmpClassReg.ysnCross = Convert.ToBoolean(reader["ysnCross"]);
                tmpClassReg.intClassNumber = Convert.ToInt32(reader["intClassNumber"]);
                tmpClassReg.strClassName = reader["strClassName"].ToString();
                
                _retVal.Add(tmpClassReg);
            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        public IEnumerable<StoreItems> GetRiderAddOns(int horseRiderID)
        {
            List<StoreItems> _retVal = new List<StoreItems>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            StoreItems tmpRiderAddOn;

            ConnectionString(objCon);

            objCom = new SqlCommand("Select intItemID, fltCost, strItemName from dbo.StoreITems where intItemID in (select intStoreItemID from dbo.RiderAddOns  where intHorseRiderID = @intHorseRiderID)", objCon);
            objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", horseRiderID));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpRiderAddOn = new StoreItems();
                tmpRiderAddOn.fltCost = Convert.ToInt32(reader["fltCost"]);
                tmpRiderAddOn.intItemID = Convert.ToInt32(reader["intItemID"]);
                tmpRiderAddOn.strItemName = reader["strItemName"].ToString();

                _retVal.Add(tmpRiderAddOn);
            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        public HorseRider GetHorseRider(int horseRiderID)
        {
            HorseRider _retVal = new HorseRider();

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ConnectionString(objCon);
            objCom = new SqlCommand("Select intPersonID, strHorseName, intRiderNumber, ysnCheckedIn, fltAmountDue, ysnHasPaid, strPaymentMethod, intDivisionID from dbo.HorseRider where intHorseRiderID = @intHorseRiderID and ysnActive = 1", objCon);
            objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", horseRiderID));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                _retVal.intHorseRiderID = horseRiderID;
                _retVal.intPersonID = Convert.ToInt32(reader["intPersonID"]);
                _retVal.strHorseName = reader["strHorseName"].ToString();
                _retVal.intRiderNumber = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]);
                _retVal.intDivisionID = Convert.ToInt32(reader["intDivisionID"]);
                _retVal.fltAmountDue = Convert.ToInt32(reader["fltAmountDue"]);
                _retVal.ysnCheckedIn = Convert.ToBoolean(reader["ysnCheckedIn"]);
                _retVal.ysnHasPaid = Convert.ToBoolean(reader["ysnHasPaid"]);
                _retVal.strPaymentMethod = reader["strPaymentMethod"].ToString();

            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public Person GetPerson(int intPersonID)
        {
            Person _retVal = new Person();

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ConnectionString(objCon);
            objCom = new SqlCommand("select strFirstName, strLastName, intAge, intPhoneNumber, strEmail, strAddress, strCity, strState, intZipCode from dbo.Person where intPersonID = @intPersonID", objCon);
            objCom.Parameters.Add(new SqlParameter("@intPersonID", intPersonID));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                _retVal.intPersonID = intPersonID;
                _retVal.strFirstName = reader["strFirstName"].ToString();
                _retVal.strLastName = reader["strLastName"].ToString();
                _retVal.intAge = DAL_Helpers.getDBIntValue(reader["intAge"]);
                _retVal.intPhoneNumber = reader["intPhoneNumber"].ToString();
                _retVal.strEmail = reader["strEmail"].ToString();
                _retVal.strAddress = reader["strAddress"].ToString();
                _retVal.strCity = reader["strCity"].ToString();
                _retVal.strState = reader["strState"].ToString();
                _retVal.intZipCode = DAL_Helpers.getDBIntValue(reader["intZipCode"]);

            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string GetDivisionName(int DivisionID)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select strDivisionName  from dbo.division  where intDivisionID = @intDIVID", objCon);
            objCom.Parameters.Add(new SqlParameter("@intDIVID", DivisionID));
            objCon.Open();
            _retVal = objCom.ExecuteScalar().ToString();
            objCon.Close();
            return _retVal;
        }

        public void DeleteRiderAddOns(int intHorseRiderId)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("Delete From dbo.RiderAddOns where intHorseRiderID = @intHorseRiderID ", objCon);
            objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", intHorseRiderId));
            objCon.Open();
            objCom.ExecuteScalar();
            objCon.Close();
        }

        public void DeleteRiderClassRegistrations(int intHorseRiderID)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("Delete From dbo.ClassRegistration where intHorseRiderID = @intHorseRiderID ", objCon);
            objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", intHorseRiderID));
            objCon.Open();
            objCom.ExecuteScalar();
            objCon.Close();
        }

        public void UpdateDivClassMapping(Class updatedClass)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("Update DivisionClassMapping set strClassName = @newClassName where intClassID = @id", objCon);
            objCom.Parameters.Add(new SqlParameter("@newClassName", updatedClass.strClassName));
            objCom.Parameters.Add(new SqlParameter("@id", updatedClass.intClassID));
            objCon.Open();
            objCom.ExecuteScalar();
            objCon.Close();

        }


        public IEnumerable<ClassRiders> GetClassRiders(int intClassNum)
        {
            List<ClassRiders> _retVal = new List<ClassRiders>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ClassRiders tmpClassRider;

            ConnectionString(objCon);

            objCom = new SqlCommand("select intRiderNumber, strHorseName, p.strFirstName, p.strLastName,cr.intHorseRiderID from dbo.HorseRider hr, dbo.Person p, dbo.ClassRegistration cr Where p.intPersonID = hr.intPersonID and " +
                " cr.intHorseRiderID = hr.intHorseRiderID and cr.intClassID = (Select intClassID from dbo.Class where intClassNumber = @classNumber) " +
                " and hr.ysnCheckedIn = 1 and hr.ysnActive = 1", objCon);
            objCom.Parameters.Add(new SqlParameter("@classNumber", intClassNum));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpClassRider = new ClassRiders();
                tmpClassRider.intRiderNumber = Convert.ToInt32(reader["intRiderNumber"]);
                tmpClassRider.intHorseRiderID = Convert.ToInt32(reader["intHorseRiderID"]);
                tmpClassRider.strHorseName =reader["strHorseName"].ToString();
                tmpClassRider.strFirstName = reader["strFirstName"].ToString();
                tmpClassRider.strLastName = reader["strLastName"].ToString();

                _retVal.Add(tmpClassRider);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public List<int> LstRiderNumbers()
        {
            List<int> _retVal = new List<int>();

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            Class tmpClass = new Class();

            ConnectionString(objCon);

            objCom = new SqlCommand("Select intRiderNumber from dbo.HorseRider where intRiderNumber is not null and ysnActive = 1", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                int tmpInt =  Convert.ToInt32(reader["intRiderNumber"]);
                _retVal.Add(tmpInt);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public void SetClassMappingInactive(DivisionClassMapping classMapping)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("Update dbo.DivisionClassMapping set ysnActive = 0  where intDivisionID = @intDivisionID and intClassID = @intClassID", objCon);
            objCom.Parameters.Add(new SqlParameter("@intDivisionID", classMapping.intDivisionID));
            objCom.Parameters.Add(new SqlParameter("@intClassID", classMapping.intClassID));
            objCon.Open();
            objCom.ExecuteScalar();
            objCon.Close();
        }
        public void SetClassMappingActive(DivisionClassMapping classMapping)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("Update dbo.DivisionClassMapping set ysnActive = 1  where intDivisionID = @intDivisionID and intClassID = @intClassID", objCon);
            objCom.Parameters.Add(new SqlParameter("@intDivisionID", classMapping.intDivisionID));
            objCom.Parameters.Add(new SqlParameter("@intClassID", classMapping.intClassID));
            objCon.Open();
            objCom.ExecuteScalar();
            objCon.Close();
        }


        public string AddUpdateClass(Class @class, string AddUpdateFlag)
        {
            string _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.CreateUpdateClass", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@addUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intClassID", ToDBNull(@class.intClassID)));
                objCom.Parameters.Add(new SqlParameter("@intClassNumber", ToDBNull(@class.intClassNumber)));
                objCom.Parameters.Add(new SqlParameter("@strCLassName", ToDBNull(@class.strClassName)));
                objCom.Parameters.Add(new SqlParameter("@fltCost", ToDBNull(@class.fltCost)));
                objCom.Parameters.Add(new SqlParameter("@intMaxNumRiders", ToDBNull(@class.intMaxNumRiders)));
                objCom.Parameters.Add(new SqlParameter("@intMinNumRiders", ToDBNull(@class.intMinNumRiders)));
                objCom.Parameters.Add(new SqlParameter("@ysnActive", ToDBNull(true)));
                objCom.Parameters.Add(new SqlParameter("@strNotes", ToDBNull(@class.strNotes)));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", ToDBNull(@class.strCreatorID))); }
                else
                    objCom.Parameters.Add(new SqlParameter("@strUserID", ToDBNull(@class.strModifierID)));
                objCom.Parameters.Add(new SqlParameter("@ysnFlag", ToDBNull(@class.ysnFlag)));
                objCom.Parameters.Add(new SqlParameter("@ysnCross", ToDBNull(@class.ysnCross)));

                objCon.Open();
                objCom.ExecuteScalar();
                //_retVal = (int)objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Good";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public void RemoveRider(HorseRider riderRemoved)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("Update HorseRider set ysnCheckedIn = @ysnCheckedIn, ysnHasPaid = @ysnPaid, ysnActive = @ysnActive where intHorseRiderID = @id", objCon);
            objCom.Parameters.Add(new SqlParameter("@id", riderRemoved.intHorseRiderID));
            objCom.Parameters.Add(new SqlParameter("@ysnActive", riderRemoved.ysnActive));
            objCom.Parameters.Add(new SqlParameter("@ysnCheckedIn", riderRemoved.ysnCheckedIn));
            objCom.Parameters.Add(new SqlParameter("@ysnPaid", riderRemoved.ysnHasPaid));
            objCon.Open();
            objCom.ExecuteScalar();
            objCon.Close();
        }



        private static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
        private static void ConnectionString(SqlConnection objCon)
        {
            objCon.ConnectionString = "Data Source=s13.winhost.com;User ID=DB_127736_trotspot_user;Password=trotSpot2019;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}