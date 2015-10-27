using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLayer
{
    public static class DbHelper
    {
        public static int ConvertToInt(object value)
        {
            return ConvertToInt32(value);
        }

        public static int ConvertToInt32(object value)
        {
            return value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }

        public static float ConvertToFloat(object value)
        {
            return ConvertToSingle(value);
        }

        public static float ConvertToSingle(object value)
        {
            return value == DBNull.Value ? 0 : Convert.ToSingle(value);
        }

        public static double ConvertToDouble(object value)
        {
            return value == DBNull.Value ? 0 : Convert.ToDouble(value);
        }

        public static decimal ConvertToDecimal(object value)
        {
            return value == DBNull.Value ? 0 : Convert.ToDecimal(value);
        }

        public static bool ConvertToBool(object value)
        {
            return value != DBNull.Value && Convert.ToBoolean(value);
        }

        public static string ConvertToString(object value)
        {
            return value == DBNull.Value ? string.Empty : Convert.ToString(value);
        }

        public static DateTime ConvertToDateTime(object value)
        {
            if (value == DBNull.Value || Convert.ToDateTime(value) == SqlDateTime.MinValue.Value)
            {
                return DateTime.MinValue;
            }
          
            return Convert.ToDateTime(value);
        }

        public static DateTime ConvertToSqlDateTime(DateTime value)
        {
            return value == DateTime.MinValue ? SqlDateTime.MinValue.Value : value;
        }
    }
}
