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
        private SportContext databaseContext;
        public Database(SportContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<bool> AddTest(Test test)
        {
            this.databaseContext.Tests.Add(test);
            return (await this.databaseContext.SaveChangesAsync()>0);
        }

        public async Task<bool> AddUser(User user)
        {
            this.databaseContext.Users.Add(user);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }
        public async Task<bool> AddAthleteToTest(Athlete a) {
            (await this.databaseContext.Tests.FindAsync(a.TestId)).Number += 1;
            this.databaseContext.Athletes.Add(a);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }
        public async Task<bool> DeleteAthlete(int userid)
        {
            var user = await this.databaseContext.Users.FindAsync(userid);
            var at = from a in this.databaseContext.Athletes where a.UserId == user.Id select a;
            foreach (var a in at) {
                (await this.databaseContext.Tests.FindAsync(a.TestId)).Number -= 1;
                this.databaseContext.Athletes.Remove(a);
            }
            this.databaseContext.Users.Remove(user);
            return (await this.databaseContext.SaveChangesAsync() > 0); 
        }

        public async Task<bool> DeleteAthleteFromTest(int aid)
        {
            var user = await this.databaseContext.Athletes.FindAsync(aid);
            (await this.databaseContext.Tests.FindAsync(user.TestId)).Number -= 1;
            this.databaseContext.Athletes.Remove(user);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteTest(int testid)
        {
            var test = await this.databaseContext.Tests.FindAsync(testid);
            var athletes = from athlete in this.databaseContext.Athletes where athlete.TestId == test.Id select athlete;
            foreach (var athlete in athletes)
            {
                this.databaseContext.Athletes.Remove(athlete);
            }
            this.databaseContext.Tests.Remove(test);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditResult(int aid, int result)
        {
            var athlete = await this.databaseContext.Athletes.FindAsync(aid);
            athlete.Result = result;
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditTest(Test test)
        {
            var testEdit = await this.databaseContext.Tests.FindAsync(test.Id);
            testEdit.Date = test.Date;
            testEdit.Type = test.Type;
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<List<User>> GetAllAthlete()
        {
            var users = from user in this.databaseContext.Users where user.Type == "Athlete" select user;
            return await users.ToAsyncEnumerable<User>().ToList<User>();
        }

        public async Task<List<Athlete>> GetAllAthleteInGivenTest(int testid)
        {
            var Athletes = from athlete in this.databaseContext.Athletes where athlete.TestId == testid select athlete;
            return await Athletes.ToAsyncEnumerable<Athlete>().ToList<Athlete>();
        }

        public async Task<List<User>> GetAllCoach()
        {
            var Coaches= from user in this.databaseContext.Users where user.Type == "Coach" select user;
            return await Coaches.ToAsyncEnumerable<User>().ToList<User>();
        }

        public async Task<List<Test>> GetAllTest(int userid)
        {
            var tests = from test in this.databaseContext.Tests where test.CoachId == userid select test;
            return await tests.ToAsyncEnumerable<Test>().ToList<Test>();
        }

        public async Task<Athlete> GetAthlete(int aid)
        {
            return await this.databaseContext.Athletes.FindAsync(aid);
        }

        public async Task<Test> GetTest(int testid)
        {
            return await this.databaseContext.Tests.FindAsync(testid);
        }

        public async Task<User> GetUser(int userid)
        {
            return await this.databaseContext.Users.FindAsync(userid);
        }
    }
}

