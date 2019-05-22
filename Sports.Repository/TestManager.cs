
using Sports.DomainModel.Models;
using System;
using System.Threading.Tasks;
using Sports.DomainModel;
using System.Collections.Generic;

namespace Sports.Repository
{
    public class TestManager:ITestManager
    {
        private readonly IDatabase _data;
        public TestManager(IDatabase _data) {
            this._data = _data;
        }
        public async Task DeleteTest(int Id)
        {
            await this._data.DeleteTest(Id);
        }

        public async Task CreateTest(Test test)
        {
            await this._data.AddTest(test);
        }

        public async Task<DetailTestModel> GetTestDetails(int Id)
        {
            var test = await this._data.GetTest(Id);
            var athleteList = await this._data.GetAllAthleteInGivenTest(Id);
            var userList = new List<UserResult>();
            foreach (var athlete in athleteList)
            {
                var newData = new UserResult() { Id = athlete.Id, Name = (await this._data.GetUser(athlete.UserId)).Name, Result = athlete.Result };
                userList.Add(newData);
            }
            var model = new DetailTestModel() { test = test, athletes = userList };
            return model;
        }

        public async Task<Test> GetTest(int Id)
        {
            return await this._data.GetTest(Id);
        }

        public async Task EditTest(Test test)
        {
            await this._data.EditTest(test);
        }

        public async Task<DashboardModel> GetTestList(string Id)
        {
            var testList = await this._data.GetAllTest(Id);
            var model= new DashboardModel() { testList = testList };
            return model;
        }

        
    }
}
