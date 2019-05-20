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
            ViewData["UserList"] = userlist;
            return View();
        }

        public IActionResult LogIn(User user) {
            
            this.SetId(user.Id);
            return RedirectToAction("Dashboard","Main");
        }

        [HttpGet]
        public IActionResult CreateCoach() {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user) {
            
            await this.data.AddUser(user);
            return RedirectToAction("Index", "Main");
        }
        public async Task<IActionResult> Dashboard() {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var testList = await this.data.GetAllTest(this.GetId());
            ViewData["TestList"] = testList;
            return View();
        }
        [HttpGet]
        public IActionResult CreateTest() {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            ViewData["userid"] = GetId();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTest(Test test) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.AddTest(test);
            return RedirectToAction("Dashboard", "Main");
        }
        public async Task<IActionResult> AthleteList() {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var list = await this.data.GetAllAthlete();
            ViewData["UserList"] = list;
            return View();
        }
        [HttpGet]
        public IActionResult CreateUser() {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.AddUser(user);
            return RedirectToAction("AthleteList", "Main");
        }
        public async Task<IActionResult> DetailTest(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var test = await this.data.GetTest(Id);
            var list = await this.data.GetAllAthleteInGivenTest(Id);
            var k = new List<UserResult>();
            foreach (var a in list) {
                var c = new UserResult() { Id = a.Id, Name = (await this.data.GetUser(a.UserId)).Name, Result = a.Result };
                k.Add(c);
            }
            ViewData["Athletes"] = k;
            ViewData["Test"] = test;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditTest(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            ViewData["test"] = await this.data.GetTest(Id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditTest(Test test) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.EditTest(test);
            return RedirectToAction("Dashboard", "Main");
        }
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
        [HttpGet]
        public async Task<IActionResult> AddAthlete(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var list = await this.data.GetAllAthlete();
            ViewData["list"] = list;
            ViewData["testid"] = Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAthlete(Athlete a) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            await this.data.AddAthleteToTest(new Athlete { TestId=a.TestId,Result=a.Result,UserId=a.UserId});
            return RedirectToAction("DetailTest", "Main",new { Id=a.TestId});
        }
        [HttpGet]
        public async Task<IActionResult> EditResultTest(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var a = await this.data.GetAthlete(Id);
            var u = new UserResult() { Id = a.Id, Name = (await this.data.GetUser(a.UserId)).Name, Result = a.Result };
            var i = a.TestId;
            ViewData["user"] = u;
            ViewData["testid"] = i;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditResultTest(UserResult user) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var a = await this.data.GetAthlete(user.Id);
            await this.data.EditResult(user.Id, user.Result);
            return RedirectToAction("DetailTest", "Main", new { Id = a.TestId });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAthleteFromTest(int Id) {
            if (!isLogedIn())
            {
                return RedirectToAction("LogOut", "Main");
            }
            var a = await this.data.GetAthlete(Id);
            await this.data.DeleteAthleteFromTest(Id);
            return RedirectToAction("DetailTest", "Main", new { Id = a.TestId });
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
