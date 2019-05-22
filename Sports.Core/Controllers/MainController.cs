using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sports.DomainModel;
using Sports.DomainModel.Models;
using Sports.Repository;

namespace Sports.Core
{

    public class MainController : Controller
    {
        private readonly IDatabase _Data;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITestManager _testManager;
        private readonly IAthleteManager _athleteManager;
        public MainController(IDatabase Data,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            this._Data = Data;
            _userManager = userManager;
            _signManager = signManager;
            this._testManager = new TestManager(_Data);
            this._athleteManager = new AthleteManager(_Data);
        }
        public IActionResult Index(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Main");
            }
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login() {
            return RedirectToAction("Index", "Main");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        Console.WriteLine("We goes here--varshesh-------------------->>>>>");
                        return RedirectToAction("Index", "Main");
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Main");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return RedirectToAction("Index", "Main");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }
        
        //no need of model
        public async Task<IActionResult> DeleteTest(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this._testManager.DeleteTest(Id);
            return RedirectToAction("Dashboard", "Main");
        }
        public async Task<IActionResult> DeleteUser(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this._athleteManager.DeleteAthlete(Id);
            return RedirectToAction("AthleteList", "Main");
        }

        //Model has been added in this methods  
        [HttpGet]
        public async Task<IActionResult> AddAthlete(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            var model = await this._athleteManager.GetAthletes(Id);
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddAthlete(AddAthleteModel model) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this._athleteManager.AddAthleteToTest(model);
            return RedirectToAction("DetailTest", "Main",new { Id= model.athlete.TestId});
        }
        public async Task<IActionResult> AthleteList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            var model = await this._athleteManager.GetAllAthlete();
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateTest()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var currentUser=await _userManager.GetUserAsync(HttpContext.User);
            var model = new CreateTestModel() { userid = currentUser.Id };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            Test test = model.test;
            await this._testManager.CreateTest(test);
            return RedirectToAction("Dashboard", "Main");
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            await this._athleteManager.AddAthlete(user);
            return RedirectToAction("AthleteList", "Main");
        }
        public async Task<IActionResult> Dashboard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var model = await this._testManager.GetTestList(currentUser.Id);
            return View(model);
        }
        public async Task<IActionResult> DetailTest(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var model = await this._testManager.GetTestDetails(Id);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditResultTest(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var model = await this._athleteManager.GetAthleteResult(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditResultTest(UserResult model) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var id= await this._athleteManager.EditAthleteResult(model);
            return RedirectToAction("DetailTest", "Main", new { Id = id });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAthleteFromTest(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var id = await this._athleteManager.DeleteAthleteFromTest(Id);
            return RedirectToAction("DetailTest", "Main", new { Id = id });
        }
        [HttpGet]
        public async Task<IActionResult> EditTest(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var model = await this._testManager.GetTest(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditTest(Test test)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            await this._testManager.EditTest(test);
            return RedirectToAction("Dashboard", "Main");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
