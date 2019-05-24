using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sports.DomainModel;
using Sports.DomainModel.Models;
using Sports.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Core.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TestController :Controller
    {
        private readonly IDatabase _Data;
        private readonly ITestManager _testManager;
        private readonly IAthleteManager _athleteManager;
        public TestController(IDatabase Data)
        {
            this._Data = Data;
            this._testManager = new TestManager(_Data);
            this._athleteManager = new AthleteManager(_Data);
        }
        //no need of model
        [Route("deletetest")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTest(int Id)
        {
            await this._testManager.DeleteTest(Id);
            return new JsonResult(new { result="success"});
        }
        [Route("deletetest")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([from]int Id)
        {
            await this._athleteManager.DeleteAthlete(Id);
            return new JsonResult(new { result = "success" });
        }

        //Model has been added in this methods  
        [HttpGet]
        public async Task<IActionResult> GetAthleteList(int Id)
        {

            var model = await this._athleteManager.GetAthletes(Id);
            return new JsonResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddAthlete(AddAthleteModel model)
        {
            await this._athleteManager.AddAthleteToTest(model);
            return RedirectToAction("DetailTest", "Main", new { Id = model.athlete.TestId });
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
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
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
        public async Task<IActionResult> EditResultTest(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var model = await this._athleteManager.GetAthleteResult(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditResultTest(UserResult model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Main");
            }
            var id = await this._athleteManager.EditAthleteResult(model);
            return RedirectToAction("DetailTest", "Main", new { Id = id });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAthleteFromTest(int Id)
        {
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
    }
}
