using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Calo4U_Sisa
{
    internal class Viikkohakija
    {
        public static void GetCurrentWeekAndYear(out int weekNumber, out int year)
        {
            DateTime today = DateTime.Today;
            weekNumber = GetIso8601WeekOfYear(today);
            year = today.Year;
        }

        // Hakee nykyisen viikon
        private static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
