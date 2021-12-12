using System;
using System.ComponentModel.DataAnnotations;

namespace TrialApplication_PeopleReport.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_of_Birth { get; set; }

        public int Age
        {
            get
            {
                int age = (int)(DateTime.Today.Year - Date_of_Birth.Year);
                if (Date_of_Birth.Date > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
