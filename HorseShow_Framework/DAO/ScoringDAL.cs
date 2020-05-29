using HorseShow_Framework.Areas.Admin.ViewModels;
using HorseShow_Framework.Areas.Admin.ViewModels.Results;
using HorseShow_Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.DAO
{
    public class ScoringDAL
    {
        public string AddUpdateClassPlacing(ClassPlacing placing, string AddUpdateFlag)
        {
            string _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.CreateUpdatePlacing", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@addUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intClassID", ToDBNull(placing.intClassID)));
                objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", ToDBNull(placing.intHorseRiderID)));
                objCom.Parameters.Add(new SqlParameter("@intPlace", ToDBNull(placing.intPlace)));
               
              
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


        public HorseRiderScores GetHorseRiderScores(int HorseRiderID)
        {
            HorseRiderScores _retVal = new HorseRiderScores();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select hs.intHorseRiderScores, hs.intHorseRiderID, hs.intOverallScore, hs.intNum1stPlace, hs.intNum2ndPlace, hs.intNum3rdPlace, hs.intNum4thPlace, hs.intNum5thPlace from HorseRiderScores hs, HorseRider hr where " +
                " hs.intHorseRiderID = hr.intHorseRiderID" +
                " and hr.ysnActive = 1  and hr.ysnCheckedIn = 1" +
                " and hr.intHorseRiderID = @id ", objCon);
            objCom.Parameters.Add(new SqlParameter("@id", HorseRiderID));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                _retVal.intHorseRiderScores = Convert.ToInt32(reader["intHorseRiderScores"]);
                _retVal.intHorseRiderID = Convert.ToInt32(reader["intHorseRiderID"]);
                _retVal.intOverallScore = Convert.ToInt32(reader["intOverallScore"]);
                _retVal.intNum1stPlace = DAL_Helpers.getDBIntValue(reader["intNum1stPlace"]);
                _retVal.intNum2ndPlace = DAL_Helpers.getDBIntValue(reader["intNum2ndPlace"]);
                _retVal.intNum3rdPlace = DAL_Helpers.getDBIntValue(reader["intNum3rdPlace"]);
                _retVal.intNum4thPlace = DAL_Helpers.getDBIntValue(reader["intNum4thPlace"]);
                _retVal.intNum5thPlace = DAL_Helpers.getDBIntValue(reader["intNum5thPlace"]);
  
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string AddUpdateHorseRiderScores(HorseRiderScores riderScore, string AddUpdateFlag)
        {
            string _retVal;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom = new SqlCommand();
                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.AddUpdateRiderScore", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@addUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intHorseRiderID", ToDBNull(riderScore.intHorseRiderID)));
                objCom.Parameters.Add(new SqlParameter("@intOverallScore", ToDBNull(riderScore.intOverallScore)));
                objCom.Parameters.Add(new SqlParameter("@int1stPlace", ToDBNull(riderScore.intNum1stPlace)));
                objCom.Parameters.Add(new SqlParameter("@int2ndPlace", ToDBNull(riderScore.intNum2ndPlace)));
                objCom.Parameters.Add(new SqlParameter("@int3rdPlace", ToDBNull(riderScore.intNum3rdPlace)));
                objCom.Parameters.Add(new SqlParameter("@int4thPlace", ToDBNull(riderScore.intNum4thPlace)));
                objCom.Parameters.Add(new SqlParameter("@int5thPlace", ToDBNull(riderScore.intNum5thPlace)));


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

        public IList<ExistingRiderPlacing> GetCurrentPlacings (int classID)
        {
            List<ExistingRiderPlacing> _retVal = new List<ExistingRiderPlacing>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            ExistingRiderPlacing tmpClass;

            ConnectionString(objCon);

            objCom = new SqlCommand("select cp.intHorseRiderID, cp.intClassID, cp.intPlace, hr.intRiderNumber from ClassPlacing cp , HorseRider hr where intClassID = @ID and cp.intHorseRiderID = hr.intHorseRiderID and hr.ysnActive = 1  and hr.ysnCheckedIn = 1", objCon);
            objCom.Parameters.Add(new SqlParameter("@ID", classID));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpClass = new ExistingRiderPlacing();
                tmpClass.intClassID = Convert.ToInt32(reader["intClassID"]);
                tmpClass.intHorseRiderID = Convert.ToInt32(reader["intHorseRiderID"]);
                tmpClass.intPlace = Convert.ToInt32(reader["intPlace"]);
                tmpClass.intRiderNumber = Convert.ToInt32(reader["intRiderNumber"]);
                _retVal.Add(tmpClass);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public IList<RiderResults> GetBestInShow(List<Division> divs)
        {
            List<RiderResults> _retVal = new List<RiderResults>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            RiderResults tmpResult;
            ConnectionString(objCon);

            foreach( var div in divs)
            {
                objCom = new SqlCommand("select TOP 1 p.strFirstName, p.strLastName, hr.intRiderNumber, hr.strHorseName, hr.intDivisionID, hrs.intOverallScore, hrs.intNum1stPlace, hrs.intNum2ndPlace, hrs.intNum3rdPlace, " +
                " hrs.intNum4thPlace, hrs.intNum5thPlace, strDivisionName  from HorseRider hr, HorseRiderScores hrs, Person p, Division d Where hr.intHorseRiderID = hrs.intHorseRiderID and hr.intPersonID = p.intPersonID " +
                " and hr.intDivisionID = d.intDivisionID and hr.intDivisionID = @id and hr.ysnActive = 1  and hr.ysnCheckedIn = 1  order by intOverallScore desc ", objCon);
                objCom.Parameters.Add(new SqlParameter("@id", div.intDivisionID));

                objCon.Open();
                SqlDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    tmpResult = new RiderResults();
                    tmpResult.strFirstName = reader["strFirstName"].ToString();
                    tmpResult.strLastName = reader["strLastName"].ToString();
                    tmpResult.strHorseName = reader["strHorseName"].ToString();
                    tmpResult.lvlID = Convert.ToInt32(reader["intDivisionID"]);
                    tmpResult.strName = reader["strDivisionName"].ToString();
                    tmpResult.intRiderNum = Convert.ToInt32(reader["intRiderNumber"]);
                    tmpResult.intTotalScore = DAL_Helpers.getDBIntValueZero(reader["intOverallScore"]);
                    tmpResult.int1stPlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum1stPlace"]);
                    tmpResult.int2PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum2ndPlace"]);
                    tmpResult.int3PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum3rdPlace"]);
                    tmpResult.int4PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum4thPlace"]);
                    tmpResult.int5PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum5thPlace"]);

                    _retVal.Add(tmpResult);
                }
                reader.Close();
                objCon.Close();
            }

            return _retVal;
        }



        public IList<RiderResults> GetDivisionBreakDowns()
        {
            List<RiderResults> _retVal = new List<RiderResults>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            RiderResults tmpResult;

            ConnectionString(objCon);

            objCom = new SqlCommand("select p.strFirstName, p.strLastName, hr.intRiderNumber, hr.strHorseName, hr.intDivisionID, hrs.intOverallScore, hrs.intNum1stPlace, hrs.intNum2ndPlace, hrs.intNum3rdPlace, " +
                " hrs.intNum4thPlace, hrs.intNum5thPlace  from HorseRider hr, HorseRiderScores hrs, Person p Where hr.intHorseRiderID = hrs.intHorseRiderID and hr.intPersonID = p.intPersonID and hr.ysnActive = 1  and hr.ysnCheckedIn = 1", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpResult = new RiderResults();
                tmpResult.strFirstName = reader["strFirstName"].ToString();
                tmpResult.strLastName = reader["strLastName"].ToString();
                tmpResult.strHorseName = reader["strHorseName"].ToString();
                tmpResult.lvlID = Convert.ToInt32(reader["intDivisionID"]);
                tmpResult.intRiderNum = Convert.ToInt32(reader["intRiderNumber"]);
                tmpResult.intTotalScore = DAL_Helpers.getDBIntValueZero(reader["intOverallScore"]);
                tmpResult.int1stPlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum1stPlace"]);
                tmpResult.int2PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum2ndPlace"]);
                tmpResult.int3PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum3rdPlace"]);
                tmpResult.int4PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum4thPlace"]);
                tmpResult.int5PlaceCount = DAL_Helpers.getDBIntValueZero(reader["intNum5thPlace"]);
               
                _retVal.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public IList<RiderResults> GetClassScores(int classNumber)
        {
            ShowDAL dAL = new ShowDAL();
            int classID = dAL.GetClassIDByNumber(classNumber);
            List<RiderResults> _retVal = new List<RiderResults>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();
            RiderResults tmpResult;

            ConnectionString(objCon);

            objCom = new SqlCommand("select p.strFirstName, p.strLastName, hr.strHorseName, hr.intDivisionID, cp.intHorseRiderID, cp.intClassID, cp.intPlace, hr.intRiderNumber from ClassPlacing cp , " +
                " HorseRider hr, Person p where intClassID = @id and cp.intHorseRiderID = hr.intHorseRiderID  and hr.intPersonID = p.intPersonID and hr.ysnActive = 1 and hr.ysnCheckedIn = 1", objCon);
            objCom.Parameters.Add(new SqlParameter("@id", classID));


            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();

            while (reader.Read())
            {
                tmpResult = new RiderResults();
                tmpResult.strFirstName = reader["strFirstName"].ToString();
                tmpResult.strLastName = reader["strLastName"].ToString();
                tmpResult.strHorseName = reader["strHorseName"].ToString();
                tmpResult.lvlID = Convert.ToInt32(reader["intDivisionID"]);
                tmpResult.intRiderNum = Convert.ToInt32(reader["intRiderNumber"]);
                tmpResult.intTotalScore = DAL_Helpers.getDBIntValueZero(reader["intPlace"]);
                
                _retVal.Add(tmpResult);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }




        public int GetPointsForPlace(int Place)
        {
            int _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom = new SqlCommand();


            ConnectionString(objCon);

            objCom = new SqlCommand("select intNumPoints from dbo.PlacePointValues  where intPlace = @place", objCon);
            objCom.Parameters.Add(new SqlParameter("@place", Place));
            objCon.Open();
            _retVal = Convert.ToInt32(objCom.ExecuteScalar());
            objCon.Close();

            return _retVal;

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