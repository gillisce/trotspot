using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework
{
    public static class DAL_Helpers
    {
        public static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }

        public static int? getDBIntValue(object value)
        {
            if (!Convert.IsDBNull(value))
                return Convert.ToInt32(value);
            else
                return null;
        }

        public static int getDBIntValueZero(object value)
        {
            if (!Convert.IsDBNull(value))
                return Convert.ToInt32(value);
            else
                return 0;
        }

        public static decimal? getDBDecValue(object value)
        {
            if (!Convert.IsDBNull(value))
                return Convert.ToDecimal(value);
            else
                return null;
        }

        public static DateTime? getDBDateValue(object value)
        {
            if (!Convert.IsDBNull(value))
                return Convert.ToDateTime(value);
            else
                return null;
        }


    }
}