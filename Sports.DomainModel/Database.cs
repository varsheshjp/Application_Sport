using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Sports.DomainModel.Models;
namespace Sports.DomainModel
{
    public class Database : IDatabase
    {
        private readonly SportContext databaseContext;
        public Database(SportContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<bool> AddTest(Test test)
        {
            this.databaseContext.Tests.Add(test);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> AddUser(User user)
        {
            this.databaseContext.mainUsers.Add(user);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }
        public async Task<bool> AddAthleteToTest(Athlete athlete)
        {
            (await this.databaseContext.Tests.FindAsync(athlete.TestId)).Number += 1;
            this.databaseContext.Athletes.Add(athlete);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }
        public async Task<bool> DeleteAthlete(int UserId)
        {
            var user = await this.databaseContext.mainUsers.FindAsync(UserId);
            var athletes = from athlete in this.databaseContext.Athletes where athlete.UserId == user.Id select athlete;
            foreach (var athlete in athletes)
            {
                (await this.databaseContext.Tests.FindAsync(athlete.TestId)).Number -= 1;
                this.databaseContext.Athletes.Remove(athlete);
            }
            this.databaseContext.mainUsers.Remove(user);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteAthleteFromTest(int athleteId)
        {
            var user = await this.databaseContext.Athletes.FindAsync(athleteId);
            (await this.databaseContext.Tests.FindAsync(user.TestId)).Number -= 1;
            this.databaseContext.Athletes.Remove(user);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteTest(int testId)
        {
            var test = await this.databaseContext.Tests.FindAsync(testId);
            var athletes = from athlete in this.databaseContext.Athletes where athlete.TestId == test.Id select athlete;
            foreach (var athlete in athletes)
            {
                this.databaseContext.Athletes.Remove(athlete);
            }
            this.databaseContext.Tests.Remove(test);
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditResult(int athleteId, int Result)
        {
            var athlete = await this.databaseContext.Athletes.FindAsync(athleteId);
            athlete.Result = Result;
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditTest(Test Test)
        {
            var testEdit = await this.databaseContext.Tests.FindAsync(Test.Id);
            testEdit.Date = Test.Date;
            testEdit.Type = Test.Type;
            return (await this.databaseContext.SaveChangesAsync() > 0);
        }

        public async Task<List<User>> GetAllAthlete()
        {
            var mainUsers = from user in this.databaseContext.mainUsers where user.Type == "Athlete" select user;
            return await mainUsers.ToAsyncEnumerable<User>().ToList<User>();
        }

        public async Task<List<Athlete>> GetAllAthleteInGivenTest(int testId)
        {
            var athletes = from athlete in this.databaseContext.Athletes where athlete.TestId == testId select athlete;
            return await athletes.ToAsyncEnumerable<Athlete>().ToList<Athlete>();
        }
        
        public async Task<List<Test>> GetAllTest(string userId)
        {
            var Tests = from test in this.databaseContext.Tests where test.CoachId == userId select test;
            return await Tests.ToAsyncEnumerable<Test>().ToList<Test>();
        }

        public async Task<Athlete> GetAthlete(int athleteId)
        {
            return await this.databaseContext.Athletes.FindAsync(athleteId);
        }

        public async Task<Test> GetTest(int testid)
        {
            return await this.databaseContext.Tests.FindAsync(testid);
        }

        public async Task<User> GetUser(int userid)
        {
            return await this.databaseContext.mainUsers.FindAsync(userid);
        }
    }
}
