using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrialApplication_PeopleReport.Models
{
    public class MonthBirthdays
    {
        public int Month { get; set; }
        public int Num_Month_Birthdays { get; set; }
        public MonthBirthdays(int month, int numMonthBirthdays)
        {
            Month = month;
            Num_Month_Birthdays = numMonthBirthdays;
        }
    }
}
