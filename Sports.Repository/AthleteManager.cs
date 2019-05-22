using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sports.DomainModel;
using Sports.DomainModel.Models;

namespace Sports.Repository
{
    public class AthleteManager : IAthleteManager
    {
        private readonly IDatabase _Data;
        public AthleteManager(IDatabase _Data) {
            this._Data = _Data;
        }
        public async Task AddAthlete(User user)
        {
            await this._Data.AddUser(user);
        }

        public async Task AddAthleteToTest(AddAthleteModel model)
        {
            await this._Data.AddAthleteToTest(new Athlete { TestId = model.athlete.TestId, Result = model.athlete.Result, UserId = model.athlete.UserId });
        }

        public async Task DeleteAthlete(int Id)
        {
            await this._Data.DeleteAthlete(Id);
        }

        public async Task<int> DeleteAthleteFromTest(int Id)
        {
            var athlete = await this._Data.GetAthlete(Id);
            await this._Data.DeleteAthleteFromTest(Id);
            return athlete.TestId;
        }

        public async Task<int> EditAthleteResult(UserResult model)
        {

            var athlete = await this._Data.GetAthlete(model.Id);
            await this._Data.EditResult(model.Id, model.Result);
            return athlete.TestId;
        }

        public async Task<AthleteListModel> GetAllAthlete()
        {
            var athleteList = await this._Data.GetAllAthlete();
            var model = new AthleteListModel() { users = athleteList };
            return model;
        }

        public async Task<UserResult> GetAthleteResult(int Id)
        {
            var athlete = await this._Data.GetAthlete(Id);
            var model = new UserResult() { Id = athlete.Id, Name = (await this._Data.GetUser(athlete.UserId)).Name, Result = athlete.Result };
            return model;
        }

        public async Task<AddAthleteModel> GetAthletes(int Id)
        {
            var model = new AddAthleteModel();
            var athleteList = await this._Data.GetAllAthlete();
            model.athletes = athleteList;
            model.testid = Id;
            return model;
        }
    }
}
