using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class EmployeesCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int EmployeeId { get; set; }

        public Course Course { get; set; }
        public Employee Employee { get; set; }
    }
}
