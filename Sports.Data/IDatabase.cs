using Sports.Core;
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
        Task<List<Test>> GetAllTest(int userid);
        Task<Test> GetTest(int testid);
        Task<List<User>> GetAllCoach();
        Task<List<User>> GetAllAthlete();
        Task<User> GetUser(int userid);
        Task<bool> AddAthleteToTest(Athlete a);
    }
    public class Database : IDatabase
    {
        private SportContext dbc;
        public Database(SportContext dbc)
        {
            this.dbc = dbc;
        }
        public async Task<bool> AddTest(Test test)
        {
            this.dbc.Tests.Add(test);
            return (await this.dbc.SaveChangesAsync()>0);
        }

        public async Task<bool> AddUser(User user)
        {
            this.dbc.Users.Add(user);
            return (await this.dbc.SaveChangesAsync() > 0);
        }
        public async Task<bool> AddAthleteToTest(Athlete a) {
            (await this.dbc.Tests.FindAsync(a.TestId)).Number += 1;
            this.dbc.Athletes.Add(a);
            return (await this.dbc.SaveChangesAsync() > 0);
        }
        public async Task<bool> DeleteAthlete(int userid)
        {
            var user = await this.dbc.Users.FindAsync(userid);
            var at = from a in this.dbc.Athletes where a.UserId == user.Id select a;
            foreach (var a in at) {
                (await this.dbc.Tests.FindAsync(a.TestId)).Number -= 1;
                this.dbc.Athletes.Remove(a);
            }
            this.dbc.Users.Remove(user);
            return (await this.dbc.SaveChangesAsync() > 0); 
        }

        public async Task<bool> DeleteAthleteFromTest(int aid)
        {
            var user = await this.dbc.Athletes.FindAsync(aid);
            (await this.dbc.Tests.FindAsync(user.TestId)).Number -= 1;
            this.dbc.Athletes.Remove(user);
            return (await this.dbc.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteTest(int testid)
        {
            var test = await this.dbc.Tests.FindAsync(testid);
            var at = from a in this.dbc.Athletes where a.TestId == test.Id select a;
            foreach (var a in at)
            {
                this.dbc.Athletes.Remove(a);
            }
            this.dbc.Tests.Remove(test);
            return (await this.dbc.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditResult(int aid, int result)
        {
            var athlete = await this.dbc.Athletes.FindAsync(aid);
            athlete.Result = result;
            return (await this.dbc.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditTest(Test test)
        {
            var t = await this.dbc.Tests.FindAsync(test.Id);
            t.Date = test.Date;
            t.Type = test.Type;
            return (await this.dbc.SaveChangesAsync() > 0);
        }

        public async Task<List<User>> GetAllAthlete()
        {
            var t = from s in this.dbc.Users where s.Type == "Athlete" select s;
            return await t.ToAsyncEnumerable<User>().ToList<User>();
        }

        public async Task<List<Athlete>> GetAllAthleteInGivenTest(int testid)
        {
            var A = from a in this.dbc.Athletes where a.TestId == testid select a;
            return await A.ToAsyncEnumerable<Athlete>().ToList<Athlete>();
        }

        public async Task<List<User>> GetAllCoach()
        {
            var A = from a in this.dbc.Users where a.Type == "Coach" select a;
            return await A.ToAsyncEnumerable<User>().ToList<User>();
        }

        public async Task<List<Test>> GetAllTest(int userid)
        {
            var A = from a in this.dbc.Tests where a.CoachId == userid select a;
            return await A.ToAsyncEnumerable<Test>().ToList<Test>();
        }

        public async Task<Athlete> GetAthlete(int aid)
        {
            return await this.dbc.Athletes.FindAsync(aid);
        }

        public async Task<Test> GetTest(int testid)
        {
            return await this.dbc.Tests.FindAsync(testid);
        }

        public async Task<User> GetUser(int userid)
        {
            return await this.dbc.Users.FindAsync(userid);
        }
    }
}

