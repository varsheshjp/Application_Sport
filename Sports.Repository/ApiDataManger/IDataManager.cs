using Sports.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Repository.ApiDataManger
{
    public interface IDataManager
    {
        //create
        Task<bool> CreatTest(Test test);
        Task<bool> CreateAthlete(User user);

        //add-edit-delete athlete from test
        Task<bool> AddNewAthleteInTest(Athlete athlete);
        Task<bool> EditAthleteInTest(Athlete athlete);
        Task<bool> DeleteAthleteFromTest(Athlete athlete);

        //add-edit-delete test
        Task<bool> DeleteTest(Test test);
        Task<bool> EditTest(Test test);
        Task<bool> AddNewTest(Test test);

        //get Test

        Task<Test> GetTestById(int testId);
        Task<List<Test>> GetTestList(string userId);

        //get Athlete from test

        Task<List<Athlete>> GetAthleteByTestId(int testId);
        Task<Athlete> GetAthleteByAthleteId(int AthleteId);

        //get Athlete as User
        Task<List<User>> GetAllUser();

        //add-delete athlete as User
        Task<bool> DeleteUser(int userId);
        

        
    }
}
