using Sports.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Repository
{
    public interface IAthleteManager
    {
        Task DeleteAthlete(int Id);
        Task<AddAthleteModel> GetAthletes(int Id);
        Task AddAthleteToTest(AddAthleteModel model);
        Task<AthleteListModel> GetAllAthlete();
        Task AddAthlete(User user);
        Task<UserResult> GetAthleteResult(int Id);
        Task<int> EditAthleteResult(UserResult model);
        Task<int> DeleteAthleteFromTest(int Id);
    }
}
