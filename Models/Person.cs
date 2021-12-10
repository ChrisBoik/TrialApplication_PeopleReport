using System;

namespace TrialApplication_PeopleReport.Models
{
    public class Person
    {
        public string First_Name { get; set; }

        public int Last_name { get; set; }

        public DateTime Date_of_Birth { get; set; }

        public int Age
        {
            get
            {
                int age = (int)(DateTime.Today.Year - Date_of_Birth.Year);
                if (Date_of_Birth > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
