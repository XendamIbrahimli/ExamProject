using ExamProject.BL.ViewModels.AgentVM;
using ExamProject.Core.Models;
using ExamProject.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =("Admin,Moderator"))]
    public class AgentController(AppDbContext _context,IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agents.Include(x=>x.Designation).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Designations = await _context.Designations.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AgentCreateVM vm)
        {
            ViewBag.Designations = await _context.Designations.ToListAsync();
            if (!vm.ImageUrl.ContentType.StartsWith("image"))
                ModelState.AddModelError("ImageUrl", "File must be image");
            if (vm.ImageUrl.Length>600*1024)
                ModelState.AddModelError("ImageUrl", "File must be less than 600kb");
            if(!ModelState.IsValid) 
                return View();

            string FileUrl=Path.GetRandomFileName()+Path.GetExtension(vm.ImageUrl.FileName);
            using(Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "photos", FileUrl)))
            {
                await vm.ImageUrl.CopyToAsync(stream);
            }

            Agent agent = new Agent()
            {
                Fullname = vm.Fullname,
                ImageUrl = FileUrl,
                DesignationId = vm.DesignationId
            };

            await _context.Agents.AddAsync(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update()
        {
            ViewBag.Designations = await _context.Designations.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(AgentUpdateVM vm, int? id)
        {
            ViewBag.Designations = await _context.Designations.ToListAsync();
            if (!ModelState.IsValid)
                return View();

            if (!vm.ImageUrl.ContentType.StartsWith("image"))
                ModelState.AddModelError("ImageUrl", "File must be image");
            if (vm.ImageUrl.Length > 600 * 1024)
                ModelState.AddModelError("ImageUrl", "File must be less than 600kb");
            if (!ModelState.IsValid)
                return View();

            if (id == null)
                return BadRequest();

            var data=await _context.Agents.FirstOrDefaultAsync(x=>x.Id == id);
            if (data == null)
                return NotFound();

            string FileUrl = Path.Combine(_env.WebRootPath, "imgs", "photos", data.ImageUrl);
            if (System.IO.File.Exists(FileUrl))
            {
                System.IO.File.Delete(FileUrl);
            }

            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "photos", FileUrl)))
            {
                await vm.ImageUrl.CopyToAsync(stream);
            }

            data.Fullname = vm.Fullname;
            data.DesignationId=vm.DesignationId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var data = await _context.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return NotFound();

            string FileUrl = Path.Combine(_env.WebRootPath, "imgs", "photos", data.ImageUrl);
            if (System.IO.File.Exists(FileUrl))
            {
                System.IO.File.Delete(FileUrl);
            }

            _context.Agents.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}
