using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Listener
    {
        public Listener()
        {
            ListenersCourses = new List<ListenersCourse>();
            Payments = new List<Payment>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }

        public ICollection<ListenersCourse> ListenersCourses { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
