using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.HelperClasses
{
    public class Security_DAL
    {
        public Boolean CheckUserEligibility(string userName, int resourceRequested)
        {
            bool _retVal = true;
            //  bool _retVal2;
            try
            {

                SqlConnection objCon = new SqlConnection();
                SqlCommand objCom;

                ConnectionString(objCon);
                objCom = new SqlCommand("dbo.[CheckUserPermission]", objCon)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                objCom.Parameters.Add(new SqlParameter("@userId", userName));
                objCom.Parameters.Add(new SqlParameter("@resourceId", resourceRequested));
                objCom.Parameters.Add("@retValue", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;
                objCon.Open();
                objCom.ExecuteScalar();
                //  _retVal = Convert.ToBoolean(objCom.ExecuteScalar());
                _retVal = Convert.ToBoolean(objCom.Parameters["@retValue"].Value);

                Console.Write("break");
            }
            catch
            {
                _retVal = false;
            }

            return _retVal;
        }

        private static void ConnectionString(SqlConnection objCon)
        {
            objCon.ConnectionString = "Data Source=s13.winhost.com;User ID=DB_127736_trotspot_user;Password=trotSpot2019;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}