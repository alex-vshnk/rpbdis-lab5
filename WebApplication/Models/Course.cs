using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Course
    {
        public Course()
        {
            EmployeesCourses = new List<EmployeesCourse>();
            ListenersCourses = new List<ListenersCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Program { get; set; }
        public string Description { get; set; }
        public int Intensity { get; set; }
        public int GroupSize { get; set; }
        public int VacanciesNumber { get; set; }
        public int HoursNumber { get; set; }
        public double Cost { get; set; }

        public ICollection<EmployeesCourse> EmployeesCourses { get; set; }
        public ICollection<ListenersCourse> ListenersCourses { get; set; }
    }
}
