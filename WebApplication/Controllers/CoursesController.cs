using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.SortStates;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CoursesController : Controller
    {
        private readonly LanguageClassesContext _context;
        private int _pageSize = 20;
        private string _currentPage = "pageCourses";
        private string _currentSortOrder = "sortOrderCourses";
        private string _currentFilterSize = "searchSizeCourses";
        private string _currentFilterHours = "searchHoursCourses";

        public CoursesController(LanguageClassesContext context)
        {
            _context = context;
        }

        // GET: Employees
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 248)]
        public IActionResult Index(SortStateCourse? sortOrder, int? searchSize, int? searchHours, int? page, bool resetFilter = false)
        {
            IQueryable<Course> courses = _context.Courses;
            sortOrder ??= GetSortStateFromSessionOrSetDefault();
            page ??= GetCurrentPageFromSessionOrSetDefault();
            if (resetFilter)
            {
                HttpContext.Session.Remove(_currentFilterSize);
                HttpContext.Session.Remove(_currentFilterHours);
            }
            searchSize ??= GetCurrentFilterSizeOrSetDefault();
            searchHours ??= GetCurrentFilterHoursOrSetDefault();
            courses = Search(courses, (SortStateCourse)sortOrder, searchSize, searchHours);
            var count = courses.Count();
            courses = courses.Skip(((int)page - 1) * _pageSize).Take(_pageSize);
            SaveValuesInSession((SortStateCourse)sortOrder, (int)page, searchSize, searchHours);
            CoursesViewModel coursesView = new CoursesViewModel()
            {
                Courses = courses,
                PageViewModel = new PageViewModel(count, (int)page, _pageSize)
            };
            return View(coursesView);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Program,Description,Intensity,GroupSize,VacanciesNumber,HoursNumber,Cost")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Program,Description,Intensity,GroupSize,VacanciesNumber,HoursNumber,Cost")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'LanguageClassesContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return _context.Courses.Any(e => e.Id == id);
        }
        private IQueryable<Course> Search(IQueryable<Course> courses,
            SortStateCourse sortOrder, int? searchSize, int? searchHours)
        {
            ViewData["searchSize"] = searchSize;
            ViewData["searchHours"] = searchHours;
            if(searchSize != null)
            {
                courses = courses.Where(c => c.GroupSize == searchSize);
            }
            if (searchHours != null)
            {
                courses = courses.Where(c => c.HoursNumber == searchHours);
            }

            ViewData["Hours"] = sortOrder == SortStateCourse.HoursAsc ? SortStateCourse.HoursDesc : SortStateCourse.HoursAsc;
            ViewData["Intensity"] = sortOrder == SortStateCourse.IntensityAsc ? SortStateCourse.IntensityDesc : SortStateCourse.IntensityAsc;
            ViewData["Cost"] = sortOrder == SortStateCourse.CostAsc ? SortStateCourse.CostDesc : SortStateCourse.CostAsc;
            ViewData["Description"] = sortOrder == SortStateCourse.DescriptionAsc ? SortStateCourse.DescriptionDesc : SortStateCourse.DescriptionAsc;
            ViewData["GroupSize"] = sortOrder == SortStateCourse.GroupSizeAsc ? SortStateCourse.GroupSizeDesc : SortStateCourse.GroupSizeAsc;
            ViewData["Name"] = sortOrder == SortStateCourse.NameAsc ? SortStateCourse.NameDesc : SortStateCourse.NameAsc;
            ViewData["Program"] = sortOrder == SortStateCourse.ProgramAsc ? SortStateCourse.ProgramDesc : SortStateCourse.ProgramAsc;
            ViewData["Vacancies"] = sortOrder == SortStateCourse.VacanciesAsc ? SortStateCourse.VacanciesDesc : SortStateCourse.VacanciesAsc;

            courses = sortOrder switch
            {
                SortStateCourse.HoursAsc => courses.OrderBy(c => c.HoursNumber),
                SortStateCourse.HoursDesc => courses.OrderByDescending(c => c.HoursNumber),
                SortStateCourse.IntensityAsc => courses.OrderBy(c => c.Intensity),
                SortStateCourse.IntensityDesc => courses.OrderByDescending(e => e.Intensity),
                SortStateCourse.CostAsc => courses.OrderBy(c => c.Cost),
                SortStateCourse.CostDesc => courses.OrderByDescending(c => c.Cost),
                SortStateCourse.DescriptionAsc => courses.OrderBy(c => c.Description),
                SortStateCourse.DescriptionDesc => courses.OrderByDescending(c => c.Description),
                SortStateCourse.GroupSizeAsc => courses.OrderBy(c => c.GroupSize),
                SortStateCourse.GroupSizeDesc => courses.OrderByDescending(c => c.GroupSize),
                SortStateCourse.NameAsc => courses.OrderBy(c => c.Name),
                SortStateCourse.NameDesc => courses.OrderByDescending(c => c.Name),
                SortStateCourse.ProgramAsc => courses.OrderBy(c => c.Program),
                SortStateCourse.ProgramDesc => courses.OrderByDescending(c => c.Program),
                SortStateCourse.VacanciesAsc => courses.OrderBy(c => c.VacanciesNumber),
                SortStateCourse.VacanciesDesc => courses.OrderByDescending(c => c.VacanciesNumber),
                SortStateCourse.No => courses.OrderBy(c => c.Id),
                _ => courses.OrderBy(e => e.Id)
            };

            return courses;
        }
        private void SaveValuesInSession(SortStateCourse sortOrder, int page, int? searchSize, int? searchHours)
        {
            HttpContext.Session.Remove(_currentSortOrder);
            HttpContext.Session.Remove(_currentPage);
            HttpContext.Session.Remove(_currentFilterSize);
            HttpContext.Session.Remove(_currentFilterHours);
            HttpContext.Session.SetString(_currentSortOrder, sortOrder.ToString());
            HttpContext.Session.SetString(_currentPage, page.ToString());
            HttpContext.Session.SetString(_currentFilterSize, searchSize.ToString());
            HttpContext.Session.SetString(_currentFilterHours, searchHours.ToString());
        }
        private SortStateCourse GetSortStateFromSessionOrSetDefault()
        {
            return HttpContext.Session.Keys.Contains(_currentSortOrder) ?
                (SortStateCourse)Enum.Parse(typeof(SortStateCourse),
                HttpContext.Session.GetString(_currentSortOrder)) : SortStateCourse.No;
        }
        private int GetCurrentPageFromSessionOrSetDefault()
        {
            return HttpContext.Session.Keys.Contains(_currentPage) ?
                Convert.ToInt32(HttpContext.Session.GetString(_currentPage)) : 1;
        }
        private int? GetCurrentFilterSizeOrSetDefault()
        {
            if (HttpContext.Session.Keys.Contains(_currentFilterSize))
            {
                try
                {
                    Convert.ToInt32(HttpContext.Session.GetString(_currentFilterSize));
                }
                catch
                {
                    return null;
                }
                return Convert.ToInt32(HttpContext.Session.GetString(_currentFilterSize));
            }
            return null;
        }

        private int? GetCurrentFilterHoursOrSetDefault()
        {
            if(HttpContext.Session.Keys.Contains(_currentFilterHours))
            {
                try
                {
                    Convert.ToInt32(HttpContext.Session.GetString(_currentFilterHours));
                }
                catch
                {
                    return null;
                }
                return Convert.ToInt32(HttpContext.Session.GetString(_currentFilterHours));
            }
            return null;
        }
    }
}
