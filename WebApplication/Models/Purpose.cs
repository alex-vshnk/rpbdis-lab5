using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Purpose
    {
        public Purpose()
        {
            Payments = new List<Payment>();
        }

        public int Id { get; set; }
        public string PurposeName { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
