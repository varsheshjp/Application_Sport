using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sports.Core;
using Sports.Data;
using Sports.Models;

namespace Sports.Controllers
{

    public class MainController : Controller
    {
        private IDatabase data;
        public MainController(IDatabase data)
        {
            this.data = data;
        }
        public int GetId()
        {
            return (int)HttpContext.Session.GetInt32("userid");
        }
        public void SetId(int id)
        {
            HttpContext.Session.SetInt32("userid", id);
        }
        public bool isLogedIn()
        {
            return (HttpContext.Session.GetInt32("userid") != null) && (HttpContext.Session.GetInt32("userid") != -1);
        }

        public async Task<IActionResult> Index()
        {
            if (isLogedIn())
            {
                return RedirectToAction("Dashboard", "Main");
            }
            List<User> userlist = await this.data.GetAllCoach();
            ViewData["Title"] = "Log In";
            var model = new IndexModel() { users = userlist };
            return View(model);
        }

        public IActionResult LogIn(IndexModel model) {
            
            this.SetId(model.user.Id);
            return RedirectToAction("Dashboard","Main");
        }
        
        //no need of model
        public async Task<IActionResult> DeleteTest(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.DeleteTest(Id);
            return RedirectToAction("Dashboard", "Main");
        }
        public async Task<IActionResult> DeleteUser(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.DeleteAthlete(Id);
            return RedirectToAction("AthleteList", "Main");
        }

        //Model has been added in this methods  
        [HttpGet]
        public async Task<IActionResult> AddAthlete(int Id) {
            if (!isLogedIn())
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
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.AddAthleteToTest(new Athlete { TestId= model.athlete.TestId,Result= model.athlete.Result,UserId= model.athlete.UserId});
            return RedirectToAction("DetailTest", "Main",new { Id= model.athlete.TestId});
        }
        public async Task<IActionResult> AthleteList()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var list = await this.data.GetAllAthlete();
            var model = new AthleteListModel() { users = list };
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateCoach()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {

            await this.data.AddUser(user);
            return RedirectToAction("Index", "Main");
        }
        [HttpGet]
        public IActionResult CreateTest()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var model = new CreateTestModel() { userid = GetId() };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestModel model)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            Test testi = model.test;
            await this.data.AddTest(testi);
            return RedirectToAction("Dashboard", "Main");
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.AddUser(user);
            return RedirectToAction("AthleteList", "Main");
        }
        public async Task<IActionResult> Dashboard()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var testList = await this.data.GetAllTest(this.GetId());
            var model = new DashboardModel() { testList = testList };
            return View(model);
        }
        public async Task<IActionResult> DetailTest(int Id)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
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
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var athlete = await this.data.GetAthlete(Id);
            var model = new UserResult() { Id = athlete.Id, Name = (await this.data.GetUser(athlete.UserId)).Name, Result = athlete.Result };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditResultTest(UserResult model) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var athlete = await this.data.GetAthlete(model.Id);
            await this.data.EditResult(model.Id, model.Result);
            return RedirectToAction("DetailTest", "Main", new { Id = athlete.TestId });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAthleteFromTest(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var athlete = await this.data.GetAthlete(Id);
            await this.data.DeleteAthleteFromTest(Id);
            return RedirectToAction("DetailTest", "Main", new { Id = athlete.TestId });
        }
        [HttpGet]
        public async Task<IActionResult> EditTest(int Id)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var model = await this.data.GetTest(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditTest(Test test)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.EditTest(test);
            return RedirectToAction("Dashboard", "Main");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.SetInt32("userid", -1);
            return RedirectToAction("Index", "Main");
        }
    }
}
