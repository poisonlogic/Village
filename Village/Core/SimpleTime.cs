using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core
{
    public class SimpleTime
    {
        public const short SECONDS_IN_MINUTES = 60;
        public const short MINUTES_IN_HOUR = 60;
        public const short HOURS_IN_DAY = 24;
        public const short DAYS_IN_MONTH = 30;
        public const short MONTHS_IN_YEAR = 12;

        public static SimpleTime operator +(SimpleTime a, SimpleTime b)
        {
            var longA = a.ToLongForm();
            var longB = b.ToLongForm();
            return FromLongForm(longA + longB);
        }
        public static SimpleTime operator -(SimpleTime a, SimpleTime b)
        {
            var longA = a.ToLongForm();
            var longB = b.ToLongForm();
            return FromLongForm(longA - longB);
        }
        public static SimpleTime operator *(SimpleTime a, int b)
        {
            var longA = a.ToLongForm();
            var longB = (long)b;
            return FromLongForm(longA * longB);
        }
        public static SimpleTime operator /(SimpleTime a, int b)
        {
            var longA = a.ToLongForm();
            var longB = (long)b;
            return FromLongForm(longA / longB);
        }

        public static SimpleTime Now { get { return new SimpleTime(DateTime.Now); } }
        public static SimpleTime FromLongForm(long value)
        {
            var sec = value % SECONDS_IN_MINUTES;
            value = (value - sec) / SECONDS_IN_MINUTES;
            var min = value % MINUTES_IN_HOUR;
            value = (value - min) / MINUTES_IN_HOUR;
            var hour = value % HOURS_IN_DAY;
            value = (value - hour) / HOURS_IN_DAY;
            var day = value % DAYS_IN_MONTH;
            value = (value - day) / DAYS_IN_MONTH;
            var month = value % MONTHS_IN_YEAR;
            value = (value - month) / MONTHS_IN_YEAR;
            var year = value;

            return new SimpleTime((short)year, (short)month, (short)day, (short)hour, (short)min, (short)sec);
        }

        public short Second { get; private set; }
        public short Minute { get; private set; }
        public short Hour { get; private set; }
        public short Day { get; private set; }
        public short Month { get; private set; }
        public short Year { get; private set; }

        public SimpleTime()
        {

        }

        public SimpleTime(short year, short month, short day, short hour, short minute, short second)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.Second = second;
        }

        public SimpleTime(DateTime dateTime)
        {
            this.Year = (short)dateTime.Year;
            this.Month = (short)dateTime.Month;
            this.Day = (short)dateTime.Day;
            this.Hour = (short)dateTime.Hour;
            this.Minute = (short)dateTime.Minute;
            this.Second = (short)dateTime.Second;
        }

        public string ToLongString()
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}", Year, Month, Day, Hour, Minute, Second);
        }

        public long ToLongForm()
        {
            long value = default(long);
            value = (Year * MONTHS_IN_YEAR);
            value = ((value + Month) * DAYS_IN_MONTH);
            value = ((value + Day) * HOURS_IN_DAY);
            value = ((value + Hour) * MINUTES_IN_HOUR);
            value = ((value + Minute) * SECONDS_IN_MINUTES);
            value += Second;
            return value;
        }

        public void CopyTo(out SimpleTime dest)
        {
            dest = new SimpleTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second);
        }

    }
}
