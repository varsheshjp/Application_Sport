using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sports.DomainModel;
using Sports.DomainModel.Models;
using Sports.Repository;
using Sports.Repository.ApiDataManger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Core.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IDataManager _dataManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public TestController(IDataManager _dataManager, UserManager<ApplicationUser> _userManager)
        {
            this._dataManager = _dataManager;
            this._userManager = _userManager;
        }


        //create api
        [Route("createTest")]
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody]Test test)//in:form body test model out:{requestType=Create respons:success or not }
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            test.CoachId = currentUser.Id;
            var result = await _dataManager.CreatTest(test);
            return new JsonResult(new { ResponseType = "boolean", responsData = result });
        }
        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateAthlete([FromBody] User user)//in:form body User model out:{requestType=create respons success or not}
        {
            var result = await _dataManager.CreateAthlete(user);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }

        //add-edit-delete athlete in test
        [Route("addAthlete")]
        [HttpPost]
        public async Task<IActionResult> AddNewAthleteInTest([FromBody] Athlete athlete)//in:athlete model out:{requestType:add respons:success or not}
        {
            var result = await _dataManager.AddNewAthleteInTest(athlete);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });

        }
        [Route("editAthlete")]
        [HttpPost]
        public async Task<IActionResult> EditAthleteInTest([FromBody]Athlete athlete)//in:athlete model out:{requestType:edit respons:success or not}
        {
            var result = await _dataManager.EditAthleteInTest(athlete);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }
        
        [Route("deleteAthlete")]
        [HttpPost]
        public async Task<IActionResult> DeleteAthleteFromTest([FromBody]Athlete athlete)//in:athlete model out:{requestType:delete respons:success or not}
        {
            var result = await _dataManager.EditAthleteInTest(athlete);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }

        //add-edit-delete test
        public void DeleteTest()//in:test model out:{requestType:delete respons:success or not}
        {

        }
        public void EditTest()//in:test model out:{requestType:Edit respons:success or not}
        {

        }
        public void AddNewTest()//in:test model out:{requestType:add respons:success or not}
        {

        }

        //get Test
        public void GetTestById()//in:test id int out:{requestType:getSingle respons:Test model by id}
        { }
        public void GetTestList()//in:void out:{requestType:getAll respons:list of Test model }
        { }

        //get Athlete from test
        public void GetAthleteByTestId()//in:test id int out:{requestType:getSelected respons:list of Athlete model by id}
        { }
        public void GetAthleteByAthleteId()//in:athlete id int out:{requestType:getSelected respons:Athlete model by id}
        { }

        //get Athlete as User
        public void GetAllUser()//in:void out:{requestType:getAll respons:List of all User model}
        { }

        //add-delete Athlete as user
        public void DeleteUser()//in:UserId out:{requestType:Delete respons:success or not}
        { }
       
    }
}
