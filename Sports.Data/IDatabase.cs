using Sports.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Sports.Data
{
    public interface IDatabase
    {
        Task<bool> AddTest(Test test);
        Task<bool> AddUser(User user);
        Task<bool> DeleteAthlete(int userid);
        Task<bool> DeleteAthleteFromTest(int aid);
        Task<bool> DeleteTest(int testid);
        Task<bool> EditTest(Test test);
        Task<bool> EditResult(int aid,int result);
        Task<List<Athlete>> GetAllAthleteInGivenTest(int testid);
        Task<Athlete> GetAthlete(int aid);
        Task<List<Test>> GetAllTest(string userid);
        Task<Test> GetTest(int testid);
        Task<List<User>> GetAllAthlete();
        Task<User> GetUser(int userid);
        Task<bool> AddAthleteToTest(Athlete a);
    }
    
}

