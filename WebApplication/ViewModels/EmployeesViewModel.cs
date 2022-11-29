using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class EmployeesViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
