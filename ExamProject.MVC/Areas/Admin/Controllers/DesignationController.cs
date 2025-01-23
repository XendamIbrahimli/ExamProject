using ExamProject.BL.ViewModels.DesignationVM;
using ExamProject.Core.Models;
using ExamProject.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin,Moderator"))]
    public class DesignationController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Designations.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DesignationCreateVM vm)
        {
            if(!ModelState.IsValid) 
                return View();

            Designation designation=new Designation()
            {
                Name = vm.Name
            };

            await _context.Designations.AddAsync(designation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(DesignationUpdateVM vm, int? id)
        {
            if (!ModelState.IsValid)
                return View();

            if (id == null)
                return BadRequest();

            var data = await _context.Designations.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return NotFound();

            data.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var data = await _context.Designations.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return NotFound();

            _context.Designations.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
