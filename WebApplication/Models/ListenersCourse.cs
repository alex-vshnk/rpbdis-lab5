using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class ListenersCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ListenerId { get; set; }

        public Course Course { get; set; }
        public Listener Listener { get; set; }
    }
}
