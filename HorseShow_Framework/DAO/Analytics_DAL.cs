using HorseShow_Framework.Areas.Admin.ViewModels;
using HorseShow_Framework.Areas.Admin.ViewModels.Analytics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.DAO
{
    public class Analytics_DAL
    {
        public IList<Export_DT> GetAllRiders()
        {
            IList<Export_DT> _retVal = new List<Export_DT>();
            Export_DT tmp;

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ConnectionString(objCon);
            objCom = new SqlCommand("select hr.intHorseRiderID, p.strFirstName, p.strLastName, p.intAge, p.intPhoneNumber, p.strEmail, p.strAddress, p.strCity, p.strState  ,p.intZipCode, hr.strHorseName, " +
                " hr.intRiderNumber, hr.fltAmountDue, hr.ysnCheckedIn, hr.ysnHasPaid, hr.strPaymentMethod, d.strDivisionName from dbo.Person p,  " +
                " dbo.HorseRider hr, dbo.Division d where p.intPersonID = hr.intPersonID and hr.intDivisionID = d.intDivisionID and hr.ysnActive = 1", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while(reader.Read())
            {
                tmp = new Export_DT();
                tmp.HorseRider.intHorseRiderID = Convert.ToInt32(reader["intHorseRiderID"]);
                tmp.Person.strFirstName = reader["strFirstName"].ToString();
                tmp.Person.strLastName = reader["strLastName"].ToString();
                tmp.Person.intAge = DAL_Helpers.getDBIntValue(reader["intAge"]);
                tmp.Person.intPhoneNumber =reader["intPhoneNumber"].ToString();
                tmp.Person.strEmail = reader["strEmail"].ToString();
                tmp.Person.strAddress = reader["strAddress"].ToString();
                tmp.Person.strCity = reader["strCity"].ToString();
                tmp.Person.strState = reader["strState"].ToString();
                tmp.Person.intZipCode = DAL_Helpers.getDBIntValue(reader["intZipCode"]);
                tmp.HorseRider.strHorseName = reader["strHorseName"].ToString();
                tmp.HorseRider.intRiderNumber = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]);
                tmp.HorseRider.fltAmountDue = Convert.ToInt32(reader["fltAmountDue"]);
                tmp.HorseRider.ysnCheckedIn = Convert.ToBoolean(reader["ysnCheckedIn"]);
                tmp.HorseRider.ysnHasPaid = Convert.ToBoolean(reader["ysnHasPaid"]);
                tmp.HorseRider.strPaymentMethod = reader["strPaymentMethod"].ToString();
                tmp.strDivisionName = reader["strDivisionName"].ToString();
                tmp.ClassCount = DAL_Helpers.getDBIntValue(GetClassCount(tmp.HorseRider.intHorseRiderID));

                _retVal.Add(tmp);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }


        public IList<ClassPlacings_DT> Get_ClassPlacings()
        {
            IList<ClassPlacings_DT> _retVal = new List<ClassPlacings_DT>();
            ClassPlacings_DT tmp;

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ConnectionString(objCon);
            objCom = new SqlCommand("select c.intClassNumber, c.strClassName, cp.intPlace, intRiderNumber, strHorseName, p.strFirstName + ' '+  strLastName as Ridername from Class c, ClassPlacing cp, HorseRider hr, Person p " +
                                    " where c.intClassID = cp.intClassID"+
                                    " and cp.intHorseRiderID = hr.intHorseRiderID"+
                                    " and hr.intPersonID = p.intPersonID"+
                                    " and hr.ysnActive = 1 and hr.ysnCheckedIn = 1" +
                                    " order by intClassNumber", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                tmp = new ClassPlacings_DT();
                tmp.RiderName = reader["Ridername"].ToString();
                tmp.RiderNumber = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]);
                tmp.ClassNumber = Convert.ToInt32(reader["intClassNumber"]);
                tmp.ClassName = reader["strClassName"].ToString();
                tmp.Place = Convert.ToInt32(reader["intPlace"]);
                tmp.HorseName = reader["strHorseName"].ToString();
                _retVal.Add(tmp);
            }
            reader.Close();
            objCon.Close();

            return _retVal;
        }

        public IList<ClassLines_Export> GetClassLineUpsExport()
        {
            IList<ClassLines_Export> _retVal = new List<ClassLines_Export>();
            ClassLines_Export tmp;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ConnectionString(objCon);
            objCom = new SqlCommand("select intClassNumber, strClassName , strDivisionName, intRiderNumber, strHorseName, p.strFirstName, p.strLastName,cr.intHorseRiderID, hr.ysnCheckedIn from dbo.HorseRider hr, dbo.Person p, dbo.ClassRegistration cr, Division d, Class c " +
                " Where p.intPersonID = hr.intPersonID and  cr.intHorseRiderID = hr.intHorseRiderID  and hr.intDivisionID = d.intDivisionID and cr.intClassID = c.intClassID " +
                " and hr.ysnActive = 1 order by intClassNumber", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                tmp = new ClassLines_Export();
                tmp.intClassNum = Convert.ToInt32(reader["intClassNumber"]);
                tmp.strClassName = reader["strClassName"].ToString();
                tmp.Division = reader["strDivisionName"].ToString();
                tmp.strFirstName = reader["strFirstName"].ToString();
                tmp.strLastName = reader["strLastName"].ToString();
                tmp.intRiderNumber = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]);
                tmp.strHorseName = reader["strHorseName"].ToString();
                tmp.intHorseRiderID  = Convert.ToInt32(reader["intHorseRiderID"]);
                tmp.CheckedIn = Convert.ToBoolean(reader["ysnCheckedIn"]);
                _retVal.Add(tmp);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }


        public IList<MasterScores> GetMasterScore()
        {
            List<MasterScores> _retVal = new List<MasterScores>();
            MasterScores tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();

            ConnectionString(objCon);

            objCom = new SqlCommand("select p.strFirstName, p.strLastName, hr.intRiderNumber, hr.strHorseName, hr.intDivisionID, d.strDivisionName, hrs.intOverallScore, hrs.intNum1stPlace, hrs.intNum2ndPlace, hrs.intNum3rdPlace, " +
                " hrs.intNum4thPlace, hrs.intNum5thPlace  from HorseRider hr, HorseRiderScores hrs, Person p, Division d" +
                " Where hr.intHorseRiderID = hrs.intHorseRiderID  and hr.intDivisionID = d.intDivisionID" +
                " and hr.intPersonID = p.intPersonID and hr.ysnActive = 1  and hr.ysnCheckedIn = 1 order by strDivisionName, intOverallScore DESC", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpResult = new MasterScores
                {
                    strFirstName = reader["strFirstName"].ToString(),
                    strLastName = reader["strLastName"].ToString(),
                    strHorseName = reader["strHorseName"].ToString(),
                    lvlID = Convert.ToInt32(reader["intDivisionID"]),
                    DivisionName = reader["strDivisionName"].ToString(),
                    intRiderNum = Convert.ToInt32(reader["intRiderNumber"]),
                    intTotalScore = DAL_Helpers.getDBIntValueZero(reader["intOverallScore"]),
                    int1stPlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum1stPlace"]),
                    int2PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum2ndPlace"]),
                    int3PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum3rdPlace"]),
                    int4PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum4thPlace"]),
                    int5PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum5thPlace"])
                };

                _retVal.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }


        public IList<GeneralInfo> GetGeneralRiderData()
        {
            List<GeneralInfo> _retVal = new List<GeneralInfo>();
            GeneralInfo tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();

            ConnectionString(objCon);

            objCom = new SqlCommand("select hr.intHorseRiderID, p.intAge, p.strCity, p.strState, p.intZipCode, strPaymentMethod, d.strDivisionName  from Person p, HorseRider hr, Division d " +
                "where p.intPersonID = hr.intPersonID and hr.intDivisionID = d.intDivisionID", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            { 

                tmpResult = new GeneralInfo
                {
                    PersonID = Convert.ToInt32(reader["intHorseRiderID"]),
                    DivisionName = reader["strDivisionName"].ToString(),
                    PaymentMethod = reader["strPaymentMethod"].ToString(),
                    City = reader["strCity"].ToString(),
                    State = reader["strState"].ToString(),
                    Age = Convert.ToInt32(reader["intAge"]),
                    ZipCode = DAL_Helpers.getDBIntValue(reader["intZipCode"]),
                };
                tmpResult.ClassCount = GetClassCount(tmpResult.PersonID);

                _retVal.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public ShowStats GetShowStats()
        {
            ShowStats _retVal = new ShowStats();
            _retVal.numRidersByDivision = GetRiderCountByDivision();
            _retVal.numRidersDuplicatName = GetDuplicateRiderNames();
            _retVal.amountSpentByRider = GetAmountSpentRider();
            _retVal.classCountByRider = ClassCountByRider();
            _retVal.ridersInClass = GetRidersInEachClass();
            return _retVal;
        }


        private IList<ValuePairs> GetRiderCountByDivision()
        {
            List<ValuePairs> _ret = new List<ValuePairs>();
            ValuePairs tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);

            objCom = new SqlCommand("select count(hr.intHorseRiderID) as count, d.strDivisionName from HorseRider hr, Division d where hr.ysnActive = 1 and hr.intDivisionID = d.intDivisionID group by strDivisionName", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {

                tmpResult = new ValuePairs
                {
                    count = DAL_Helpers.getDBIntValueZero(reader["count"]),
                    Description = reader["strDivisionName"].ToString()
                    
                };
                _ret.Add(tmpResult);    
            }
            reader.Close();
            objCon.Close();

            return _ret;
        }

        private IList<ValuePairs> GetDuplicateRiderNames()
        {
            List<ValuePairs> _ret = new List<ValuePairs>();
            ValuePairs tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);

            objCom = new SqlCommand("select count(p.intPersonID) as [count], p.strFirstName, p.strLastName from  Person p group by p.strFirstName, p.strLastName Having count(p.intPersonID) > 1", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {

                tmpResult = new ValuePairs
                {
                    count = DAL_Helpers.getDBIntValueZero(reader["count"]),
                    Description = reader["strFirstName"].ToString() + " " + reader["strLastName"].ToString()

                };
                _ret.Add(tmpResult);    
            }
            reader.Close();
            objCon.Close();

            return _ret;
        }

        private IList<ValuePairs> GetAmountSpentRider()
        {
            List<ValuePairs> _ret = new List<ValuePairs>();
            ValuePairs tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);

            objCom = new SqlCommand("select fltAmountDue, intRiderNumber from HorseRider where ysnActive = 1 and ysnCheckedIn = 1", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {

                tmpResult = new ValuePairs
                {
                    count = DAL_Helpers.getDBIntValueZero(reader["fltAmountDue"]),
                    Description = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]).ToString()

                };
                _ret.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();

            return _ret;
        }

        private IList<ValuePairs> ClassCountByRider()
        {
            List<ValuePairs> _ret = new List<ValuePairs>();
            ValuePairs tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);

            objCom = new SqlCommand("select intRiderNumber, intHorseRiderID from HorseRider where ysnActive = 1 and ysnCheckedIn = 1", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {

                tmpResult = new ValuePairs
                {
                    Description = DAL_Helpers.getDBIntValue(reader["intRiderNumber"]).ToString()

                };
                tmpResult.count = GetClassCount(Convert.ToInt32(reader["intHorseRiderID"]));
                _ret.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();

            return _ret;
        }

        private IList<ValuePairs> GetRidersInEachClass()
        {
            List<ValuePairs> _ret = new List<ValuePairs>();
            ValuePairs tmpResult;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);

            objCom = new SqlCommand("select count(*) as [count], intClassNumber, strClassName from ClassRegistration  cr, Class c where cr.intClassID = c.intClassID group by intClassNumber, strClassName order by count(*) desc", objCon);
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {

                tmpResult = new ValuePairs
                {
                    count = DAL_Helpers.getDBIntValueZero(reader["count"]),
                    Description = Convert.ToInt32(reader["intClassNumber"]) + ": " + reader["strClassName"].ToString()

                };
                _ret.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();

            return _ret;
        }

        private int GetClassCount(int horseRiderID)
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

        private static void ConnectionString(SqlConnection objCon)
        {
            objCon.ConnectionString = "Data Source=s13.winhost.com;User ID=DB_127736_trotspot_user;Password=trotSpot2019;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}