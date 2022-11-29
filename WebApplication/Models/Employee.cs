using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Employee
    {
        public Employee()
        {
            EmployeesCourses = new List<EmployeesCourse>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Education { get; set; }
        public int PositionId { get; set; }

        public Position Position { get; set; }
        public ICollection<EmployeesCourse> EmployeesCourses { get; set; }
    }
}
