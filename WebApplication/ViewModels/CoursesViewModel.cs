using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
