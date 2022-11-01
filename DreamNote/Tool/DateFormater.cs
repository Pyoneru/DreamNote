using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNote.Tool
{
    public class DateFormater
    {
        public static string Date(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string Format(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy hh:mm:ss");
        }

        public static DateTime ConvertFormat(string dateTime)
        {
            return Convert.ToDateTime(dateTime);
        }

        public static string NoFormat(DateTime dateTime)
        {
            return dateTime.ToString("ddMMyyyyhhmmss");
        }
    }
}
