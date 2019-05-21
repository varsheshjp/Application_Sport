using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sports.Data;
using Sports.Models;

namespace Sports.Controllers
{

    public class MainController : Controller
    {
        private IDatabase data;
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;
        public MainController(IDatabase data,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            this.data = data;
            _userManager = userManager;
            _signManager = signManager;
        }

        public async Task<IActionResult> Index(string returnUrl = "")
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
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Main");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
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
            await this.data.DeleteTest(Id);
            return RedirectToAction("Dashboard", "Main");
        }
        public async Task<IActionResult> DeleteUser(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.DeleteAthlete(Id);
            return RedirectToAction("AthleteList", "Main");
        }

        //Model has been added in this methods  
        [HttpGet]
        public async Task<IActionResult> AddAthlete(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            var model = new AddAthleteModel();
            var list = await this.data.GetAllAthlete();
            model.athletes = list;
            model.testid = Id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddAthlete(AddAthleteModel model) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.AddAthleteToTest(new Athlete { TestId= model.athlete.TestId,Result= model.athlete.Result,UserId= model.athlete.UserId});
            return RedirectToAction("DetailTest", "Main",new { Id= model.athlete.TestId});
        }
        public async Task<IActionResult> AthleteList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOut", "Main");
            }
            var list = await this.data.GetAllAthlete();
            var model = new AthleteListModel() { users = list };
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
            Test testi = model.test;
            await this.data.AddTest(testi);
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
            await this.data.AddUser(user);
            return RedirectToAction("AthleteList", "Main");
        }
        public async Task<IActionResult> Dashboard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var testList = await this.data.GetAllTest(currentUser.Id);
            var model = new DashboardModel() { testList = testList };
            return View(model);
        }
        public async Task<IActionResult> DetailTest(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var test = await this.data.GetTest(Id);
            var list = await this.data.GetAllAthleteInGivenTest(Id);
            var userList = new List<UserResult>();
            foreach (var athlete in list)
            {
                var newData = new UserResult() { Id = athlete.Id, Name = (await this.data.GetUser(athlete.UserId)).Name, Result = athlete.Result };
                userList.Add(newData);
            }
            var model = new DetailTestModel() { test = test, athletes = userList };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditResultTest(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var athlete = await this.data.GetAthlete(Id);
            var model = new UserResult() { Id = athlete.Id, Name = (await this.data.GetUser(athlete.UserId)).Name, Result = athlete.Result };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditResultTest(UserResult model) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var athlete = await this.data.GetAthlete(model.Id);
            await this.data.EditResult(model.Id, model.Result);
            return RedirectToAction("DetailTest", "Main", new { Id = athlete.TestId });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAthleteFromTest(int Id) {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var athlete = await this.data.GetAthlete(Id);
            await this.data.DeleteAthleteFromTest(Id);
            return RedirectToAction("DetailTest", "Main", new { Id = athlete.TestId });
        }
        [HttpGet]
        public async Task<IActionResult> EditTest(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var model = await this.data.GetTest(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditTest(Test test)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            await this.data.EditTest(test);
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
