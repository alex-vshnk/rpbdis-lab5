using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class PositionsViewModel
    {
        public IEnumerable<Position> Positions { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
