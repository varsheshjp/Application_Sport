using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
            Console.WriteLine("------------------------------------------------>>>>");
            Console.WriteLine(athlete.Id);
            Console.WriteLine(athlete.TestId);
            var result = await _dataManager.DeleteAthleteFromTest(athlete);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }

        //add-edit-delete-get test
        [Route("deleteTest")]
        [HttpPost]
        public async Task<IActionResult> DeleteTest(Test test)//in:test model out:{requestType:delete respons:success or not}
        {
            var result = await this._dataManager.DeleteTest(test);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }
        [Route("editTest")]
        [HttpPost]
        public async Task<IActionResult> EditTest(Test test)//in:test model out:{requestType:Edit respons:success or not}
        {
            var result = await this._dataManager.EditTest(test);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }
        [HttpGet("getTest/{testid:int}")]
        public async Task<IActionResult> GetTestById(int testId)//in:test id int out:{requestType:getSingle respons:Test model by id}
        {
            return new JsonResult(await this._dataManager.GetTestById(testId));
        }
        [Route("getTestList")]
        public async Task<IActionResult> GetTestList()//in:void out:{requestType:getAll respons:list of Test model }
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return new JsonResult(await this._dataManager.GetTestList(currentUser.Id));
        }
        [Route("createTest")]
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody]Test test)//in:form body test model out:{requestType=Create respons:success or not }
        {
            Console.WriteLine("------------------------------------------------>>>>");
            Console.WriteLine(test.Date);
            Console.WriteLine(test.Type);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            test.CoachId = currentUser.Id;
            var result = await _dataManager.CreatTest(test);
            return new JsonResult(new { ResponseType = "boolean", responsData = result });
        }

        //get Athlete from test
        [HttpGet("getAthleteByTest/{testid:int}")]
        public async Task<IActionResult>  GetAthleteByTestId(int testId)//in:test id int out:{requestType:getSelected respons:list of Athlete model by id}
        {
            return new JsonResult(await this._dataManager.GetAthleteByTestId(testId));
        }
        [HttpGet("getAthleteById/{Aid:int}")]
        public async Task<IActionResult> GetAthleteByAthleteId(int Aid)//in:athlete id int out:{requestType:getSelected respons:Athlete model by id}
        {
            return new JsonResult(await this._dataManager.GetAthleteByAthleteId(Aid));
        }

        //get-add-delete Athlete as user
        [Route("getAthleteList")]
        public async Task<IActionResult> GetAllUser()//in:void out:{requestType:getAll respons:List of all User model}
        {
            return new JsonResult(await this._dataManager.GetAllUser());
        }
        [HttpGet("deleteAthleteUser/{userid:int}")]
        public async Task<IActionResult> DeleteUser(int userid)//in:UserId out:{requestType:Delete respons:success or not}
        {
            return new JsonResult(await this._dataManager.DeleteUser(userid));
        }
        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateAthlete([FromBody] User user)//in:form body User model out:{requestType=create respons success or not}
        {
            var result = await _dataManager.CreateAthlete(user);
            return new JsonResult(new { ResponsType = "boolean", responsData = result });
        }


    }
}
