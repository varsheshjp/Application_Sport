using Sports.DomainModel;
using Sports.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Repository.ApiDataManger
{
    public class DataManager : IDataManager
    {
        private readonly IDatabase _data;
        public DataManager(IDatabase _data) {
            this._data = _data;
        }

        //create
        public async Task<bool> CreatTest(Test test) {
            return await this._data.AddTest(test);

        }
        public async Task<bool> CreateAthlete(User user) {
            return await this._data.AddUser(user);
        }

        //add-edit-delete athlete from test
        public async Task<bool> AddNewAthleteInTest(Athlete athlete) {
            return await this._data.AddAthleteToTest(athlete);
        }
        public async Task<bool> EditAthleteInTest(Athlete athlete) {
            return await this._data.EditResult(athlete.Id, athlete.Result);
        }
        public async Task<bool> DeleteAthleteFromTest(Athlete athlete) {
            return await this._data.DeleteAthleteFromTest(athlete.Id);
        }

        //add-edit-delete test
        public async Task<bool> DeleteTest(Test test) {
            return await this._data.DeleteTest(test.Id);
        }

        public async Task<bool> EditTest(Test test) {
            return await this._data.EditTest(test);
        }
        public async Task<bool> AddNewTest(Test test) {
            return await this._data.AddTest(test);
        }

        //get Test

        public async Task<Test> GetTestById(int testId) {
            return await this._data.GetTest(testId);
        }
        public async Task<List<Test>> GetTestList(string userId) {
            return await this._data.GetAllTest(userId); 
        }

        //get Athlete from test

        public async  Task<List<Athlete>> GetAthleteByTestId(int testId) {
            return await this._data.GetAllAthleteInGivenTest(testId);
        }
        public async Task<Athlete> GetAthleteByAthleteId(int AthleteId)
        {
            return await this._data.GetAthlete(AthleteId);
        }

        //get Athlete as User
        public async Task<List<User>> GetAllUser() {
            return await this._data.GetAllAthlete();
        }

        //add-delete athlete as User
        public async Task<bool> DeleteUser(int userId) {
            return await this._data.DeleteAthlete(userId);
        }
        
    }
}
