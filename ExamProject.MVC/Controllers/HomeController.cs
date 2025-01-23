using ExamProject.BL.ViewModels.Home;
using ExamProject.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.MVC.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM();
            vm.Agents = await _context.Agents.Include(x => x.Designation).ToListAsync();
            return View(vm);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
