using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int ListenerId { get; set; }
        public int PurposeId { get; set; }

        public Listener Listener { get; set; }
        public Purpose Purpose { get; set; }
    }
}
