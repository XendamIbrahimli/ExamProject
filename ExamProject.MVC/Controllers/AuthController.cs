using ExamProject.BL.Enums;
using ExamProject.BL.ViewModels;
using ExamProject.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ExamProject.MVC.Controllers
{
    public class AuthController(UserManager<User> _userManager,SignInManager<User> _signInManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) 
                return View();

            User user=new User()
            {
                UserName=vm.Username,
                Email=vm.Email
            };

            var result=await _userManager.CreateAsync(user,vm.Password);
            if (!result.Succeeded) 
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            var result1 = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            if (!result1.Succeeded)
            {
                foreach (var item in result1.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM VM)
        {
            if (!ModelState.IsValid) 
                return View();

            User user = null;
            if (VM.UsernameOrEmail.Contains("@"))
                user=await _userManager.FindByEmailAsync(VM.UsernameOrEmail);
            else
                user=await _userManager.FindByNameAsync(VM.UsernameOrEmail);


            if (user == null)
                ModelState.AddModelError("", "Username or Password is wrong");

            var result=await _signInManager.PasswordSignInAsync(user,VM.Password,false,false);
            if (!result.Succeeded)
            {
                if(result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
